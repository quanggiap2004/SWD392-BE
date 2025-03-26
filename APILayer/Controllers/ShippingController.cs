using Application.Services.Interfaces;
using Common.Model.ShippingDTOs.Request;
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

        [HttpPost("fee")]
        public async Task<IActionResult> CalculateFeeShip([FromBody] ShippingFeeRequestDTO shippingFeeRequest)
        {
            var result = await _shippingService.GetShippingFeeAsync(shippingFeeRequest);
            return Ok(result);
        }

        [HttpPut("/order/{orderId}")]
        public async Task<IActionResult> ShipOrder(int orderId, int status)
        {
            try
            {
                var result = await _shippingService.UpdateOrderStatusForShipping(orderId, status);
                if (result)
                {
                    return Ok("Update shipping status sucessfully");
                }
                else
                {
                    return BadRequest("Update shpping status sucessfully failed");
                }
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
