using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddRangeOrderItems(ICollection<OrderItem> orderItems);
        Task<bool> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList);
    }
}
