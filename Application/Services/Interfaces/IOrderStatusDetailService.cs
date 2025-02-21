using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;

namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IOrderStatusDetailService
    {
        Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail);
        Task<bool> CheckOrderStatusDetailExist(int orderId, int statusId);
    }
}
