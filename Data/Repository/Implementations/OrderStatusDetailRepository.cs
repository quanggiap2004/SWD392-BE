using AutoMapper;
using Common.Model.OrderStatusDetailDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class OrderStatusDetailRepository : IOrderStatusDetailRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        private readonly IMapper _mapper;
        public OrderStatusDetailRepository(BlindBoxSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail)
        {
            var result = await _context.OrderStatusDetails.Where(x => x.OrderId == orderStatusDetail.orderId && x.OrderStatusId == orderStatusDetail.statusId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.OrderStatusUpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            OrderStatusDetail statusDetail = new OrderStatusDetail
            {
                OrderId = orderStatusDetail.orderId,
                OrderStatusId = orderStatusDetail.statusId,
                OrderStatusNote = orderStatusDetail.note,
                OrderStatusUpdatedAt = orderStatusDetail.updatedAt
            };
            _context.OrderStatusDetails.Add(statusDetail);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRangeOrderStatusDetailAsync(List<OrderStatusDetailSimple> orderStatusDetailSimplesList)
        {
            var result = _mapper.Map<List<OrderStatusDetail>>(orderStatusDetailSimplesList);
            await _context.AddRangeAsync(result);
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
