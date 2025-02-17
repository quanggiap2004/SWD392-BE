using BlindBoxSystem.Domain.Model.AuthenticationDTO;

namespace BlindBoxSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
        Task<bool> ResetPassword(string newPassword, string email);
    }
}
