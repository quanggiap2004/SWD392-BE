using Common.Model.BoxItemDTOs.Response;
using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Domain.Domain.Entities;

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
        Task<BoxItemResponseDto> ProcessOnlineSerieBoxOrder(CreateOrderDTO model, int orderId);
        Task<bool> UpdateOnlineSerieBoxTotalPrice(int orderId);
        Task<bool> UpdateOrderForShipping(int orderId);
        Task<bool> UpdateOrderPendingStatus();
    }
}
