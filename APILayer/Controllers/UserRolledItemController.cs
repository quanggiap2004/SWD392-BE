using Application.Services.Interfaces;
using Common.Model.UserRolledItemDTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolledItemController : ControllerBase
    {
        private readonly IUserRolledItemService _userRolledItemService;
        public UserRolledItemController(IUserRolledItemService userRolledItemService)
        {
            _userRolledItemService = userRolledItemService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserRolledItemDto>>> getAllUserRolledItemById(int userId)
        {
            try
            {
                var userRolledItems = await _userRolledItemService.GetUserRolledItemsByUserId(userId);
                return Ok(userRolledItems);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
