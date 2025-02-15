using BlindBoxSystem.Domain.Model.AccountDTOs;

namespace BlindBoxSystem.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
    }
}
