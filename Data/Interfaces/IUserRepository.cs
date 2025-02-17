using BlindBoxSystem.Domain.Model.AuthenticationDTO;

namespace BlindBoxSystem.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
        Task<bool> ResetPassword(string newPassword, string email);
    }
}
