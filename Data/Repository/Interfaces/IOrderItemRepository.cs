using Common.Model.OrderItem.Response;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddRangeOrderItems(ICollection<OrderItem> orderItems);
        Task<OrderItem?> GetOrderItemById(int orderItemId);
        Task<OpenRequestResponseDto?> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList);
    }
}
