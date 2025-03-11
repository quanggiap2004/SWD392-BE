using Common.Model.UserRolledItemDTOs;
using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IUserRolledItemService
    {
        Task AddUserRolledItemAsync(UserRolledItem userRolledItem);
        Task<IEnumerable<UserRolledItemDto>> GetUserRolledItemsByUserId(int userId);
        Task<bool> UpdateUserRolledItemCheckoutStatus(List<int> currentUserRolledItemIds, bool status);
    }
}
