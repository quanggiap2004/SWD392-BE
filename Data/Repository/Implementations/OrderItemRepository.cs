using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                return false;
            }
            orderItem.OrderStatusCheckCardImage = imageList;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
