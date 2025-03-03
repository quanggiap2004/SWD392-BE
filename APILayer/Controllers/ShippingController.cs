using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IShippingService _shippingService;
        private readonly IOrderService _orderService;
        public ShippingController(IShippingService shippingService, IOrderService orderService)
        {
            _shippingService = shippingService;
            _orderService = orderService;
        }
        // GET: api/<ShippingController>
        [HttpGet("shops")]
        public async Task<IActionResult> GetShops()
        {
            var result = await _shippingService.GetShopsAsync();
            return Ok(result);
        }




    }
}
