using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpPost]
        public async Task<ActionResult> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList)
        {
            try
            {
                var result = await _orderItemService.UpdateOpenBlindBoxForCustomerImage(orderItemId, imageList);
                return Ok("Upload image successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
