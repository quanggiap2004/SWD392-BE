using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderResponseDto> AddOrder(CreateOrderDtoDetail model);
        Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId);
        Task<IEnumerable<Order>> GetAllOrderForDasboard();
        Task<ManageOrderDto?> GetOrderById(int orderId);
        Task<DraftOrderDto> SaveDraftOrder(string jsonModel, CreateOrderDTO model);
        Task<bool> UpdateCurrentStatus(int orderId, int statusId);
        Task<Order?> GetOrderDto(int orderId);
        Task<OrderResponseDto> UpdateVnPayOrder(CreateOrderDtoDetail createOrderDtoDetail, int orderId);
        Task UpdateShippingFeeAndAddress(int orderId, decimal shippingFee, int addressId);
        Task<bool> UpdateOnlineSerieBoxAfterShip(int orderId);
        Task<bool> SaveChangesAsync();
        Task<Order> GetOrderEntityById(int orderId);
        Task<bool> UpdateCodOrdersPendingStatus();
    }
}
