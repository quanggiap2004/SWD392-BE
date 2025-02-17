using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlindBoxSystem.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("access successfully");
        }
    }
}
