using Application.Services.Interfaces;
using Common.Model.OrderItem.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/order-item")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
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

        [HttpPut("{id}/refund")]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult> RefundBoxItem(int id, RefundOrderItemRequestDto request)
        {
            try
            {
                var result = await _orderItemService.UpdateDataAfterRefund(request, id);
                return Ok("Refund box item successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/refund-request")]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult> UpdateRefundRequest(int id)
        {
            try
            {
                var result = await _orderItemService.UpdateRefundRequest(id);
                return Ok("Update refund request successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}/refund/details")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateRefundDetails(
            int id,
            [FromBody] UpdateOrderItemRefundDetailsRequestDto request)
        {
            try
            {
                var result = await _orderItemService.UpdateRefundDetails(id, request);
                return Ok("Refund details updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
