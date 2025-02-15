using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Model.AccountDTOs;

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
    }
}
