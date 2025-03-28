using Application.Services.Interfaces;
using Common.Constants;
using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Common.Model.PaymentDTOs.Response;
using Domain.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User, Admin, Staff")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public PaymentController(IVnPayService vnPayService, IOrderService orderService, IUserService userService, IEmailService emailService)
        {
            _vnPayService = vnPayService;
            _orderService = orderService;
            _userService = userService;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpPost("make-payment")]
        public async Task<ActionResult> MakePayment([FromBody] CreateOrderDTO model)
        {
            try
            {
                if (model.paymentMethod == ProjectConstant.COD)
                {
                    OrderResponseDto result = await _orderService.CreateOrderCOD(model);
                    return Ok(result);
                }
                var response = await _orderService.SaveDraftOrder(model);
                return Ok(_vnPayService.CreatePaymentUrl(model, HttpContext, response.orderId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
                var orderForOnlineSerieBox = await _orderService.GetOrderById(paymentResponse.OrderId);
                if (orderDto.orderItemRequestDto.First().isOnlineSerieBox)
                {
                    if (orderForOnlineSerieBox.shippingFee > 0)
                    {
                        var updateOnlineSerieBoxResult = await _orderService.UpdateOnlineSerieBoxTotalPrice(paymentResponse.OrderId);
                        if(!updateOnlineSerieBoxResult)
                        {
                            return BadRequest(new { message = "Update online serie box total price failed" });
                        }
                        return Ok(paymentResponse);
                    }
                    return Ok(await _orderService.ProcessOnlineSerieBoxOrder(orderDto, paymentResponse.OrderId));
                }
                OrderResponseDto result = await _orderService.UpdateOrderVnPay(orderDto, paymentResponse.OrderId);

                var user = await _userService.GetUserById(orderDto.userId);
                if (user != null)
                {
                    await _emailService.SendPaymentConfirmationEmailAsync(user.email, user.fullname, paymentResponse.OrderId);
                }
                if (orderDto.orderItemRequestDto.Count == 1 && orderDto.orderItemRequestDto.First().isOnlineSerieBox == true && orderDto.subTotal != 0)
                {
                    return Ok(new
                    {
                        BoxOptionId = orderDto.orderItemRequestDto.First().boxOptionId,
                        IsOnlineSerieBox = orderDto.orderItemRequestDto.First().isOnlineSerieBox,
                    });
                }
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
