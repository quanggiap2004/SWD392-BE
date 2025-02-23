using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddRangeOrderItems(ICollection<OrderItem> orderItems);
    }
}
