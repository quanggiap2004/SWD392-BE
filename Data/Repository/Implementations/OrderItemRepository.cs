using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;

namespace Data.Repository.Implementations
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public OrderItemRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }
        public async Task AddRangeOrderItems(ICollection<OrderItem> orderItems)
        {
            await _context.OrderItems.AddRangeAsync(orderItems);
            await _context.SaveChangesAsync();
        }
    }
}
