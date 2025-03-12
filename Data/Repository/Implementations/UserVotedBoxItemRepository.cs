using Common.Model.UserVotedBoxItemDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class UserVotedBoxItemRepository : IUserVotedBoxItemRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public UserVotedBoxItemRepository(BlindBoxSystemDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<UserVotedBoxItem>> GetAllVoted()
        {
            return await _context.UserVotedBoxItems.Include(b => b.BoxItem).Include(u => u.User).ToListAsync();
        }

        public async Task<UserVotedBoxItem> AddOrUpdateVoteAsync(AddVoteDTO addVoteDTO)
        {
            var userVotedBoxItem = await _context.UserVotedBoxItems
                .FirstOrDefaultAsync(uv => uv.UserId == addVoteDTO.UserId && uv.BoxItemId == addVoteDTO.BoxItemId);

            if (userVotedBoxItem == null)
            {
                userVotedBoxItem = new UserVotedBoxItem
                {
                    UserId = addVoteDTO.UserId,
                    BoxItemId = addVoteDTO.BoxItemId,
                    Rating = addVoteDTO.Rating,
                    LastUpdated = DateTime.UtcNow
                };
                _context.UserVotedBoxItems.Add(userVotedBoxItem);
            }
            else
            {
                userVotedBoxItem.Rating = addVoteDTO.Rating;
                userVotedBoxItem.LastUpdated = DateTime.UtcNow;
                _context.UserVotedBoxItems.Update(userVotedBoxItem);
            }

            await _context.SaveChangesAsync();
            await UpdateBoxItemAverageRating(addVoteDTO.BoxItemId);
            return userVotedBoxItem;
        }

        private async Task UpdateBoxItemAverageRating(int boxItemId)
        {
            var boxItem = await _context.BoxItems
                    .Include(b => b.UserVotedBoxItems)
                    .FirstOrDefaultAsync(b => b.BoxItemId == boxItemId);

            if (boxItem != null)
            {
                var averageRating = boxItem.UserVotedBoxItems.Average(uv => uv.Rating);
                boxItem.AverageRating = (int)averageRating;
                await _context.SaveChangesAsync();
                Console.WriteLine($"Updated Average Rating for BoxItemId {boxItemId}: {averageRating}");
            }
        }

        public async Task<IEnumerable<UserVotedBoxItem>> GetVotesByBoxItemIdAsync(int boxItemId)
        {
            return await _context.UserVotedBoxItems
                .Where(uv => uv.BoxItemId == boxItemId)
                .Include(u => u.User)
                .ToListAsync();
        }
    }
}
