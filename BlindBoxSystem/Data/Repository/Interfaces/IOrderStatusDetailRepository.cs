using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IOrderStatusDetailRepository
    {
        Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail);
        Task<bool> CheckOrderStatusDetailExist(int orderId, int statusId);
    }
}
