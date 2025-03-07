using Common.Constants;
using Common.Model.OrderItem.Response;
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

        public async Task<OrderItem?> GetOrderItemById(int orderItemId)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == orderItemId);
        }

        public async Task<OpenRequestResponseDto?> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList)
        {
            var orderItem = await _context.OrderItems.Include(x => x.Order).FirstOrDefaultAsync(x => x.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                return null;
            }
            if (orderItem.OpenRequestNumber == imageList.Count)
            {
                orderItem.Order.OpenRequest = false;
                orderItem.Order.CurrentOrderStatusId = (int)ProjectConstant.OrderStatus.Shipping;
            }
            orderItem.OrderStatusCheckCardImage = imageList;
            await _context.SaveChangesAsync();
            return new OpenRequestResponseDto
            {
                orderId = orderItem.OrderId,
                orderItemId = orderItemId,
            };
        }
    }
}
