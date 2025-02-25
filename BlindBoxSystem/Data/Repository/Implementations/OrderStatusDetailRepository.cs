using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Repository.Implementations
{
    public class OrderStatusDetailRepository : IOrderStatusDetailRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public OrderStatusDetailRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail)
        {
            OrderStatusDetail statusDetail = new OrderStatusDetail
            {
                OrderId = orderStatusDetail.orderId,
                OrderStatusId = orderStatusDetail.statusId,
                OrderStatusNote = orderStatusDetail.note,
                OrderStatusUpdatedAt = DateTime.UtcNow
            };
            _context.OrderStatusDetails.Add(statusDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckOrderStatusDetailExist(int orderId, int statusId)
        {
            var result = await _context.OrderStatusDetails.Where(x => x.OrderId == orderId && x.OrderStatusId == statusId).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
