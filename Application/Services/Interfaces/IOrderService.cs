using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId);

        Task<bool> CancelOrder(int orderId, string note);
        Task<OrderResponseDto> CreateOrderCOD(CreateOrderDTO model);
        Task<DraftOrderDto> SaveDraftOrder(CreateOrderDTO model);
        Task<CreateOrderDTO> GetOrderDto(int orderId);
        Task<OrderResponseDto> UpdateOrderVnPay(CreateOrderDTO orderDto, int orderId);
        Task<ManageOrderDto> GetOrderById(int orderId);
        Task<bool> UpdateOrderForShipping(int orderId, int status);
        Task<bool> UpdateOrderPendingStatus();
    }
}
