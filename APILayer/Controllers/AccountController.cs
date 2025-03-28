using Application.Services.Interfaces;
using Common.Constants;
using Common.Model.AuthenticationDTO;
using Domain.Domain.Entities.ApplicationEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APILayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IUserService userService, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userService = userService;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.userName,
                Email = model.email,
                PhoneNumber = model.phoneNumber,
                FullName = model.fullName,
                Password = model.password
            };
            if (model.isTestAccount)
            {
                user.EmailConfirmed = true;
                model.isActive = true;
            }
            var result = await _userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                string roleName = model.roleId switch
                {
                    1 => ProjectConstant.ADMIN,
                    2 => ProjectConstant.STAFF,
                    3 => ProjectConstant.USER,
                    _ => null
                };

                if (roleName != null)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole(roleName));
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                    await _userService.AddUser(model);
                }

                // Generate email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
                var confirmationLink = $"{_configuration["FrontendUrl"]}/confirm-email?token={token}&email={user.Email}";
                // Send email
                await _emailService.SendRegistrationConfirmationEmailAsync(user.Email, confirmationLink, user.FullName);

                return Ok(new { message = "Registered successful. Please check your email to confirm your account." });
            }
            return BadRequest(new { message = result.Errors });
        }

        [AllowAnonymous]
        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.password))
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return Unauthorized(new { message = "Email not confirmed. Please check your email to confirm your account." });
                }
                var userRoles = await _userManager.GetRolesAsync(user);
                var userDetail = await _userService.GetUserByEmail(user.Email);
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("username", user.UserName),
                    new Claim("userId", userDetail.userId.ToString())
                };

                authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)
                );
                var userLoginResponse = await _userService.GetUserByEmail(user.Email);

                if (userLoginResponse == null)
                {
                    return BadRequest(new { message = "User not found" });
                }

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    user = userLoginResponse
                });
            }
            return Unauthorized(new { message = "Invalid email or password." });
        }

        [AllowAnonymous]
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new ApplicationRole(role));
                if (result.Succeeded)
                {
                    return Ok(new { message = "Role created successfully" });
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(new { message = "Role already exists" });
        }

        [AllowAnonymous]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                return BadRequest(new { message = "Role not found" });
            }
            var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role assigned successfully" });
            }
            return BadRequest(result.Errors);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userAccount = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid email address" });
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _userService.UpdateIsActiveStatus(userAccount.userId, true);
                return Ok(new { message = "Email confirmed successfully" });
            }

            return BadRequest(new { message = "Email confirmation failed" });
        }

        [AllowAnonymous]
        [HttpPost("resend-verification-email")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(new { message = "Email is already confirmed." });
            }

            // Generate a new email confirmation token
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);

            // Send email
            await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");

            return Ok(new { message = "Verification email resent. Please check your email to confirm your account." });
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Generate password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{_configuration["FrontendUrl"]}/reset-password?token={token}&email={user.Email}";

            // Send email
            await _emailService.SendEmailAsync(user.Email, "Reset your password", $"Please reset your password by clicking this link: <a href='{resetLink}'>link</a>");

            return Ok(new { message = "Password reset email sent. Please check your email to reset your password. " + token });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.token, model.newPassword);
            var saveNewPassword = await _userService.ResetPassword(model.newPassword, model.email);

            if (result.Succeeded)
            {
                return Ok(new { message = "Password reset successfully" });
            }
            return BadRequest(new { message = "Password reset failed", errors = result.Errors });
        }

        [AllowAnonymous]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDto.email);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.currentPassword, changePasswordDto.newPassword);

            if (result.Succeeded)
            {
                bool saveNewPassword = await _userService.ChangePassword(changePasswordDto);
                return Ok(new { message = "Password changed successfully" });
            }

            return BadRequest(new { message = "Password change failed", errors = result.Errors });
        }

        [AllowAnonymous]
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleAuthenticationModel model)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={model.credentialToken}");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Failed to verify token.");
            }

            var content = await response.Content.ReadAsStringAsync();
            JObject userInfo = JObject.Parse(content);

            var name = userInfo["given_name"]?.ToString();
            var fullName = userInfo["name"]?.ToString();
            var email = userInfo["email"]?.ToString();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = fullName,
                    Password = "Giap123456",
                    EmailConfirmed = true
                };
                RegisterAccountDTO registerAccountDTO = new RegisterAccountDTO
                {
                    email = email,
                    userName = email,
                    fullName = fullName,
                    password = "Giap123456",
                    roleId = 3,
                    phoneNumber = "0943991995",
                    isActive = true
                };
                await _userManager.CreateAsync(user, user.Password);
                var userLogin = await _userManager.FindByEmailAsync(email);
                if (userLogin == null)
                {
                    return BadRequest(new { message = "create user failed" });
                }
                await _userManager.AddToRoleAsync(userLogin, ProjectConstant.USER);
                await _userService.AddUser(registerAccountDTO);
            }
            var userLoginResponse = await _userService.GetUserByEmail(user.Email);
            if (userLoginResponse == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.UserName),
                new Claim("userId", user.Id)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha256)
            );



            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                user = userLoginResponse
            });
        }

    }
}
