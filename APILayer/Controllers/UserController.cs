using Application.Services.Interfaces;
using Domain.Domain.Model.UserDTO.Request;
using Domain.Domain.Model.UserDTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    //[Authorize(Roles = "User, Admin, Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserById([FromRoute] int id)
        {
            try
            {
                UserProfile userProfile = await _userService.GetUserById(id);
                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user-by-email/{email}")]
        public async Task<ActionResult<UserLoginResponse>> GetUserByEmail([FromRoute] string email)
        {
            try
            {
                UserLoginResponse userLoginResponse = await _userService.GetUserByEmail(email);
                return Ok(userLoginResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto userProfile)
        {
            try
            {
                var result = await _userService.UpdateUserProfile(userProfile);
                if (result)
                {
                    return Ok("User profile updated successfully.");
                }
                return BadRequest("Username duplicate please check again");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("all-users")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
