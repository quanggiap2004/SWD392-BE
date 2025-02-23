using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItems(ICollection<OrderItem> orderItems);
    }
}
