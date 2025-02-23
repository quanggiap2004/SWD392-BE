using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;
using BlindBoxSystem.Domain.Model.PaymentDTOs.Response;

namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(CreateOrderDTO model, HttpContext context, int orderId);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
