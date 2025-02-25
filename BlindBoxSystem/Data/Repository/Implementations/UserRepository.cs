using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.AuthenticationDTO;
using BlindBoxSystem.Domain.Model.UserDTO.Request;
using BlindBoxSystem.Domain.Model.UserDTO.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlindBoxSystem.Data.Repository.Implementations
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

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == changePasswordDto.email);
            if (user != null)
            {
                if (user.Password != changePasswordDto.currentPassword)
                {
                    return false;
                }
                _context.Entry(user).State = EntityState.Modified;
                user.Password = changePasswordDto.newPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User?> GetByCondition(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.FirstOrDefaultAsync(expression);
        }

        public async Task<UserLoginResponse?> GetUserByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking().Where(u => u.Email.Equals(email)).Select(u => new UserLoginResponse
            {
                userId = u.UserId,
                fullname = u.Fullname,
                phone = u.Phone,
                email = u.Email,
                username = u.Username,
                roleId = u.RoleId,
                gender = u.Gender,
                isActive = u.IsActive,
            }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> ResetPassword(string newPassword, string email)
        {
            var user = await GetByCondition(e => e.Email == email);
            if (user != null)
            {
                _context.Entry(user).State = EntityState.Modified;
                user.Password = newPassword;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<UserProfile?> GetUserById(int id)
        {
            var user = await _context.Users.AsNoTracking().Where(u => u.UserId == id && u.IsActive == true).Select(u => new UserProfile
            {
                fullname = u.Fullname,
                phone = u.Phone,
                email = u.Email,
                username = u.Username,
                roleId = u.RoleId,
                gender = u.Gender,
                isActive = u.IsActive
            }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> UpdateUserProfile(UpdateUserProfileDto userProfile)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(userProfile.email));
            if (user != null)
            {
                user.Username = userProfile.username;
                user.Fullname = userProfile.fullname;
                user.Phone = userProfile.phone;
                user.Gender = userProfile.gender;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
