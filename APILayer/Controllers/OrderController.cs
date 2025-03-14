﻿using Application.Services.Interfaces;
using Common.Model.OrderDTOs.Response;
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
        public async Task<ActionResult<ICollection<ManageOrderDto>>> GetAllOrders(int userId)
        {
            var result = await _orderService.GetAllOrders(userId);
            return Ok(result);
        }

        [HttpPut("cancel/{orderId}")]
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
    }
}
