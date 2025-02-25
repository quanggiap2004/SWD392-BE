using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItems(ICollection<OrderItem> orderItems);
    }
}
