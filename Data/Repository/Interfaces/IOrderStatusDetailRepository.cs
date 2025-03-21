using Common.Model.OrderStatusDetailDTOs;

namespace Data.Repository.Interfaces
{
    public interface IOrderStatusDetailRepository
    {
        Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail);
        Task<bool> AddRangeOrderStatusDetailAsync(List<OrderStatusDetailSimple> orderStatusDetailSimplesList);
        Task<bool> CheckOrderStatusDetailExist(int orderId, int statusId);
    }
}
