using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetDashboardData()
        {
            var data = await _dashboardService.GetDashboardDataAsync();
            return Ok(data);
        }

        [HttpGet("bestSellers")]
        public async Task<IActionResult> GetBestSellersBox()
        {
            var data = await _dashboardService.GetBestSellerBoxForDashboard();
            return Ok(data);
        }
    }
}
