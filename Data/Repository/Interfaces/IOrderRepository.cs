using BlindBoxSystem.Domain.Model.OrderDTOs.Response;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId);
        Task<ManageOrderDto?> GetOrderById(int orderId);
        Task<bool> UpdateCurrentStatus(int orderId, int statusId);
    }
}
