using Common.Model.AuthenticationDTO;
using Common.Model.UserDTO.Request;
using Common.Model.UserDTO.Response;
using Domain.Domain.Entities;
using System.Linq.Expressions;

namespace Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
        Task<bool> ResetPassword(string newPassword, string email);
        Task<UserLoginResponse> GetUserByEmail(string email);
        Task<User?> GetByCondition(Expression<Func<User, bool>> expression);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<UserProfile?> GetUserById(int id);
        Task<bool> UpdateUserProfile(UpdateUserProfileDto userProfile);
        Task<IEnumerable<UserProfile>> GetAllUsers();
        Task DeleteUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateIsActiveStatus(int userId, bool status);
    }
}
