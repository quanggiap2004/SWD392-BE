using BlindBoxSystem.Domain.Model.AccountDTOs;

namespace BlindBoxSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(RegisterAccountDTO registerAccountDTO);
    }
}
