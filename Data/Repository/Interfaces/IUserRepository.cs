using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.AuthenticationDTO;
using BlindBoxSystem.Domain.Model.UserDTO.Request;
using BlindBoxSystem.Domain.Model.UserDTO.Response;
using System.Linq.Expressions;

namespace BlindBoxSystem.Data.Repository.Interfaces
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
    }
}
