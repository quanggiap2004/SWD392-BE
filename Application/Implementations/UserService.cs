using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Model.AuthenticationDTO;

namespace BlindBoxSystem.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> AddUser(RegisterAccountDTO registerAccountDTO)
        {
            return await _userRepository.AddUser(registerAccountDTO);
        }

        public async Task<bool> ResetPassword(string newPassword, string email)
        {
            return await _userRepository.ResetPassword(newPassword, email);
        }
    }
}
