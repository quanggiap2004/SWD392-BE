using Common.Model.AuthenticationDTO;
using Common.Model.UserDTO.Request;
using Common.Model.UserDTO.Response;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
        Task<bool> ResetPassword(string newPassword, string email);
        Task<UserLoginResponse> GetUserByEmail(string email);
        Task<bool> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<UserProfile?> GetUserById(int id);
        Task<bool> UpdateUserProfile(UpdateUserProfileDto userProfile);
        Task<IEnumerable<UserProfile>> GetAllUsers();
    }
}
