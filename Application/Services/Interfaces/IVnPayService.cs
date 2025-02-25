using Domain.Domain.Model.OrderDTOs.Request;
using Domain.Domain.Model.PaymentDTOs.Response;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Interfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(CreateOrderDTO model, HttpContext context, int orderId);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
