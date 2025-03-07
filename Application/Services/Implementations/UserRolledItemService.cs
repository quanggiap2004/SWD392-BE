using Application.Services.Interfaces;
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
    }
}
