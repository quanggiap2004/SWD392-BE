using Domain.Domain.Entities;
using Domain.Domain.Model.AuthenticationDTO;
using Domain.Domain.Model.UserDTO.Request;
using Domain.Domain.Model.UserDTO.Response;
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
    }
}
