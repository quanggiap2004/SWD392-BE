using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Model.AuthenticationDTO;
using BlindBoxSystem.Domain.Model.UserDTO.Request;
using BlindBoxSystem.Domain.Model.UserDTO.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using static BlindBoxSystem.Common.Exceptions.CustomExceptions;

namespace BlindBoxSystem.Application.Services.Implementations
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

        public async Task<bool> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            return await _userRepository.ChangePassword(changePasswordDto);
        }

        public async Task<UserLoginResponse> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if(user == null)
            {
                throw new NotFoundException("Can't find the user with email: " + email);
            }
            return user;
        }

        public async Task<UserProfile?> GetUserById(int id)
        {
            UserProfile userProfile = await _userRepository.GetUserById(id);
            if (userProfile == null)
            {
                throw new NotFoundException("Can't find account, the user account might be unactive or not exist");
            }
            return userProfile;
        }

        public async Task<bool> ResetPassword(string newPassword, string email)
        {
            return await _userRepository.ResetPassword(newPassword, email);
        }
    }
}
