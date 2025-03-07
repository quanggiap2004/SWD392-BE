using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IUserRolledItemRepository
    {
        Task AddUserRolledItemAsync(UserRolledItem userRolledItem);
    }
}
