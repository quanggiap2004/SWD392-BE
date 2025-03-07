using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IUserRolledItemService
    {
        Task AddUserRolledItemAsync(UserRolledItem userRolledItem);
    }
}
