using Common.Constants;
using Common.Model.OrderItem.Request;
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
            return await _context.OrderItems.Include(x => x.Order).FirstOrDefaultAsync(x => x.OrderItemId == orderItemId);
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
            }
            orderItem.OrderStatusCheckCardImage = imageList;
            await _context.SaveChangesAsync();
            return new OpenRequestResponseDto
            {
                orderId = orderItem.OrderId,
                orderItemId = orderItemId,
            };
        }

        public async Task<bool> UpdateDataAfterRefund(RefundOrderItemRequestDto request, int orderItemId)
        {
            // Retrieve the order item with its associated order.
            var orderItem = await _context.OrderItems
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(x => x.OrderItemId == orderItemId);

            if (orderItem == null)
            {
                return false;
            }

            if (orderItem.Quantity < request.numOfRefund)
            {
                throw new Exception("Refund quantity is greater than the quantity of the order item");
            }

            orderItem.NumOfRefund = request.numOfRefund;
            orderItem.RefundStatus = ProjectConstant.RefundResolved;

            await _context.SaveChangesAsync();

            var newRevenue = orderItem.Order.Revenue - request.numOfRefund * orderItem.OrderPrice;

            bool hasPendingRefunds = await _context.OrderItems
                .Where(x => x.OrderId == orderItem.OrderId && x.OrderItemId != orderItem.OrderItemId)
                .AnyAsync(x => x.RefundStatus == ProjectConstant.RefundRequest);

            orderItem.Order.RefundRequest = hasPendingRefunds;
            orderItem.Order.Revenue = newRevenue;
            orderItem.Order.OrderUpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateRefundRequest(int id)
        {
            var result = await _context.OrderItems.Include(oi => oi.Order).Where(oi => oi.OrderItemId == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            result.Order.RefundRequest = true;
            result.Order.OrderUpdatedAt = DateTime.UtcNow;
            result.RefundStatus = ProjectConstant.RefundRequest;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
