using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;

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
    }
}
