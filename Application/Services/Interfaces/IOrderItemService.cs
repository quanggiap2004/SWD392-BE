using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItems(ICollection<OrderItem> orderItems);
        Task<bool> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList);
    }
}
