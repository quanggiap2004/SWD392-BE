using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Common.Constants;
using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;
using BlindBoxSystem.Domain.Model.PaymentDTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlindBoxSystem.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderService _orderService;
        public PaymentController(IVnPayService vnPayService, IOrderService orderService)
        {
            _vnPayService = vnPayService;
            _orderService = orderService;
        }

        [AllowAnonymous]
        [HttpPost("make-payment")]
        public async Task<ActionResult> MakePayment([FromBody] CreateOrderDTO model)
        {
            if (model.paymentMethod == ProjectConstant.COD)
            {
                OrderResponseDto result = await _orderService.CreateOrderCOD(model);
                return Ok(result);
            }
            var response = await _orderService.SaveDraftOrder(model);
            return Ok(_vnPayService.CreatePaymentUrl(model, HttpContext, response.orderId));
        }

        [AllowAnonymous]
        [HttpPost("payment-callback")]
        public async Task<IActionResult> PaymentCallBack()
        {
            // Read the URL-encoded form data from the request body.
            var formData = await Request.ReadFormAsync();

            IQueryCollection queryCollection = new QueryCollection(formData.ToDictionary(k => k.Key, v => v.Value));

            // Call the service function to decode and validate the VNPay response.
            PaymentResponseModel paymentResponse = _vnPayService.PaymentExecute(queryCollection);

            if (!paymentResponse.VnPayResponseCode.Equals("00"))
            {
                return BadRequest("Payment failed");
            }
        
            // Return a response based on the validation result.
            if (paymentResponse.Success)
            {
                CreateOrderDTO orderDto = await _orderService.GetOrderDto(paymentResponse.OrderId);
                OrderResponseDto result = await _orderService.UpdateOrderVnPay(orderDto, paymentResponse.OrderId);
                // Optionally, update order status or perform other business logic.
                return Ok(paymentResponse);
            }
            else
            {
                return BadRequest(paymentResponse);
            }
        }
    }
}
