using Common.Model.UserRolledItemDTOs.Request;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IUserRolledItemRepository
    {
        Task<UserRolledItem> AddUserRolledItemAsync(UserRolledItem userRolledItem);
        Task<IEnumerable<UserRolledItemDto>> GetUserRolledItemsByUserId(int userId);
        Task<bool> UpdateUserRolledItemCheckoutStatus(List<int> currentUserRolledItemIds, bool status);
    }
}
