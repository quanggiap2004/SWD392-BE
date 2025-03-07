using Common.Model.CurrentRolledITemDTOs.Request;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class CurrentRolledItemRepository : ICurrentRolledItemRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public CurrentRolledItemRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCurrentRolledItem(CurrentRolledItem currentRolledItem)
        {
            await _context.CurrentRolledItems.AddAsync(currentRolledItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CurrentRolledItemDto>> GetCurrentRolledItemsByOnlineSerieBoxId(int onlineSerieBoxId)
        {
            return await _context.CurrentRolledItems.Where(o => o.OnlineSerieBoxId == onlineSerieBoxId && o.IsDisable == false).Select(o => new CurrentRolledItemDto
            {
                onlineSerieBoxId = o.OnlineSerieBoxId,
                boxItemId = o.BoxItem.BoxItemId,
            }).ToListAsync();
        }

        public async Task ResetCurrentRoll(int onlineSerieBoxId)
        {
            var rolledItems = await _context.CurrentRolledItems
            .Where(o => o.OnlineSerieBoxId == onlineSerieBoxId)
            .ToListAsync();

            rolledItems.ForEach(o => o.IsDisable = true);

            await _context.SaveChangesAsync();

        }
    }
}
