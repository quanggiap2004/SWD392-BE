using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;

namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId);

        Task<bool> CancelOrder(int orderId, string note);
        Task<OrderResponseDto> CreateOrderCOD(CreateOrderDTO model);
        Task<DraftOrderDto> SaveDraftOrder(CreateOrderDTO model);
        Task<CreateOrderDTO> GetOrderDto(int orderId);
        Task<OrderResponseDto> UpdateOrderVnPay(CreateOrderDTO orderDto, int orderId);
    }
}
