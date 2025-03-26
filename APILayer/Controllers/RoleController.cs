using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Staff")]
    [ApiController]
    public class RoleController : ControllerBase
    {
    }
}
