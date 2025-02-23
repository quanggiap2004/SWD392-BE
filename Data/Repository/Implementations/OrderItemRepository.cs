using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Repository.Implementations
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
