using BlindBoxSystem.Domain.Model.AuthenticationDTO;
using BlindBoxSystem.Domain.Model.UserDTO.Response;

namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
        Task<bool> ResetPassword(string newPassword, string email);
        Task<UserLoginResponse> GetUserByEmail(string email);
    }
}
