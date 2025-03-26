using Application.Services.Interfaces;
using Common.Model.OrderDTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderSerivce)
        {
            _orderService = orderSerivce;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult<ICollection<ManageOrderDto>>> GetAllOrders(int userId)
        {
            var result = await _orderService.GetAllOrders(userId);
            return Ok(result);
        }

        [HttpPut("cancel/{orderId}")]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult> CancelOrder(int orderId, string? note)
        {
            try
            {
                var result = await _orderService.CancelOrder(orderId, note);
                if (result == false)
                {
                    return BadRequest("Cancel order failed");
                }
                return Ok("Cancel order successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult> GetOrderById (int id)
        {
            try
            {
                var result = await _orderService.GetOrderById(id);
                return Ok(result);
            }catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
