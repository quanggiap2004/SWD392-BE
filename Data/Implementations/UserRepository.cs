using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.AccountDTOs;

namespace BlindBoxSystem.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BlindBoxSystemDbContext _context;

        public UserRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(RegisterAccountDTO registerAccountDTO)
        {
            var user = new User
            {
                Email = registerAccountDTO.email,
                Username = registerAccountDTO.userName,
                Gender = registerAccountDTO.gender,
                Fullname = registerAccountDTO.fullName,
                Phone = registerAccountDTO.phoneNumber,
                Password = registerAccountDTO.password,
                RoleId = registerAccountDTO.roleId,
            };

            _context.Users.Add(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
