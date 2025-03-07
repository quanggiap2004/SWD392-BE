using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderResponseDto> AddOrder(CreateOrderDtoDetail model);
        Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId);
        Task<ManageOrderDto?> GetOrderById(int orderId);
        Task<DraftOrderDto> SaveDraftOrder(string jsonModel, CreateOrderDTO model);
        Task<bool> UpdateCurrentStatus(int orderId, int statusId);
        Task<Order?> GetOrderDto(int orderId);
        Task<OrderResponseDto> UpdateVnPayOrder(CreateOrderDtoDetail createOrderDtoDetail, int orderId);
    }
}
