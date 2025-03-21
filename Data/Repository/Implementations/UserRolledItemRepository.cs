using Common.Model.BoxItemDTOs.Response;
using Common.Model.UserRolledItemDTOs.Request;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class UserRolledItemRepository : IUserRolledItemRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public UserRolledItemRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }
        public async Task AddUserRolledItemAsync(UserRolledItem userRolledItem)
        {
            await _context.UserRolledItems.AddAsync(userRolledItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRolledItemDto>> GetUserRolledItemsByUserId(int userId)
        {
            var result = await _context.UserRolledItems
                .Where(x => x.UserId == userId && x.IsCheckOut == false)
                .Select(x => new UserRolledItemDto
                {
                    id = x.UserRolledItemId,
                    userId = x.UserId,
                    userName = x.User.Username,
                    boxOptionId = x.OnlineSerieBox.BoxOption.BoxOptionId,
                    isCheckout = x.IsCheckOut,
                    boxOptionName = x.OnlineSerieBox.BoxOption.BoxOptionName,
                    brandId = x.BoxItem.Box.BrandId,
                    brandName = x.BoxItem.Box.Brand.BrandName,
                    isOnlineSerieBox = true,
                    originPrice = 0,
                    price = 0,
                    quantity = 1,
                    boxItem = new BoxItemResponseDto
                    {
                        boxItemId = x.BoxItem.BoxItemId,
                        boxItemName = x.BoxItem.BoxItemName,
                        boxItemDescription = x.BoxItem.BoxItemDescription,
                        boxItemColor = x.BoxItem.BoxItemColor,
                        averageRating = x.BoxItem.AverageRating,
                        imageUrl = x.BoxItem.ImageUrl,
                        numOfVote = x.BoxItem.NumOfVote,
                        isSecret = x.BoxItem.IsSecret
                    }
                })
                .ToListAsync();
            return result;
        }

        public async Task<bool> UpdateUserRolledItemCheckoutStatus(List<int> currentUserRolledItemIds, bool status)
        {
            var affectedRows = await _context.UserRolledItems.Where(x => currentUserRolledItemIds.Contains(x.UserRolledItemId)).
                ExecuteUpdateAsync(setters => setters.SetProperty(x => x.IsCheckOut, status));
            return affectedRows > 0;
        }
    }
}
