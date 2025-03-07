using Common.Model.OrderStatusDetailDTOs;

namespace Application.Services.Interfaces
{
    public interface IOrderStatusDetailService
    {
        Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail);
        Task<bool> CheckOrderStatusDetailExist(int orderId, int statusId);
    }
}
