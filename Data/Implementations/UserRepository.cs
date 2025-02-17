using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.AuthenticationDTO;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Principal;

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

        public async Task<User?> GetByCondition(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.FirstOrDefaultAsync(expression);
        }

        public async Task<bool> ResetPassword(string newPassword, string email)
        {
            var user = await GetByCondition(e => e.Email == email);
            if(user != null)
            {
                _context.Entry(user).State = EntityState.Modified;
                user.Password = newPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
