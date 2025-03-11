using Application.Services.Interfaces;
using Common.Exceptions;
using Common.Model.UserRolledItemDTOs.Request;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;

namespace Application.Services.Implementations
{
    public class UserRolledItemService : IUserRolledItemService
    {
        private readonly IUserRolledItemRepository _userRolledItemRepository;
        public UserRolledItemService(IUserRolledItemRepository userRolledItemRepository)
        {
            _userRolledItemRepository = userRolledItemRepository;
        }
        public Task AddUserRolledItemAsync(UserRolledItem userRolledItem)
        {
            return _userRolledItemRepository.AddUserRolledItemAsync(userRolledItem);
        }

        public async Task<IEnumerable<UserRolledItemDto>> GetUserRolledItemsByUserId(int userId)
        {
            var result = await _userRolledItemRepository.GetUserRolledItemsByUserId(userId);
            if(result == null)
            {
                throw new CustomExceptions.NotFoundException("User id not found");
            }
            return result;
        }

        public async Task<bool> UpdateUserRolledItemCheckoutStatus(List<int> currentUserRolledItemIds, bool status)
        {
            return await _userRolledItemRepository.UpdateUserRolledItemCheckoutStatus(currentUserRolledItemIds, status);
        }
    }
}
