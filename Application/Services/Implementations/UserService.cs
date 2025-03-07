using Application.Services.Interfaces;
using Common.Model.AuthenticationDTO;
using Common.Model.UserDTO.Request;
using Common.Model.UserDTO.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Entities.ApplicationEntities;
using Microsoft.AspNetCore.Identity;
using static Common.Exceptions.CustomExceptions;

namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            if (user == null)
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

        public async Task<bool> UpdateUserProfile(UpdateUserProfileDto userProfile)
        {
            var user = await _userManager.FindByEmailAsync(userProfile.email);
            if (user == null)
            {
                throw new NotFoundException("Can't find the user with email: " + userProfile.email);
            }
            user.UserName = userProfile.username;
            user.FullName = userProfile.fullname;
            user.PhoneNumber = userProfile.phone;
            var identityUpdate = await _userManager.UpdateAsync(user);
            if (!identityUpdate.Succeeded)
            {
                return false;
            }
            var userUpdate = await _userRepository.UpdateUserProfile(userProfile);
            if (userUpdate == false)
            {
                return false;
            }
            return true;
        }

        public async Task<IEnumerable<UserProfile>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
    }
}
