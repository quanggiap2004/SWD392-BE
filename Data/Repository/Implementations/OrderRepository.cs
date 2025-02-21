using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;
using BlindBoxSystem.Domain.Model.OrderItem;
using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;
using BlindBoxSystem.Domain.Model.VoucherDTOs.Response;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public OrderRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId)
        {
            var result = await _context.Orders
                .Where(o => userId <= 0|| o.UserId == userId)
                .Select(o => new ManageOrderDto
                {
                    userId = o.UserId,
                    orderId = o.OrderId,
                    orderCreatedAt = o.OrderCreatedAt,
                    orderStatusDetailsSimple = o.OrderStatusDetails.Select(osd => new OrderStatusDetailSimple
                    {
                        orderId = osd.OrderId,
                        statusName = osd.OrderStatus.OrderStatusName,
                        note = osd.OrderStatusNote,
                        updatedAt = osd.OrderStatusUpdatedAt
                    }).ToList(),
                    addressId = o.AddressId,
                    openRequest = o.OpenRequest,
                    refundRequest = o.RefundRequest,
                    paymentMethod = o.PaymentMethod,
                    totalPrice = o.TotalPrice,
                    currentStatusId = o.CurrentOrderStatusId,
                    voucher = new VoucherDto
                    {
                        voucherId = o.VoucherId,
                        voucherDiscount = o.Voucher.VoucherDiscount,
                    },
                    orderItems = o.OrderItems.Select(oi => new OrderItemSimpleDto
                    {
                        orderItemId = oi.OrderItemId,
                        boxOptionId = oi.BoxOptionId,
                        quantity = oi.Quantity,
                        orderPrice = oi.OrderPrice,
                        isFeedback = oi.IsFeedback,
                        orderStatusCheckCardImage = oi.OrderStatusCheckCardImage,
                        isRefund = oi.IsRefund,
                        openRequest = oi.OpenRequest

                    }).ToList()
                }).ToListAsync();

            return result;
        }

        public async Task<bool> UpdateCurrentStatus(int orderId, int statusId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.CurrentOrderStatusId = statusId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<ManageOrderDto?> GetOrderById(int orderId)
        {
            return await _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new ManageOrderDto
                {
                    userId = o.UserId,
                    orderId = o.OrderId,
                    orderCreatedAt = o.OrderCreatedAt,
                    orderStatusDetailsSimple = o.OrderStatusDetails.Select(osd => new OrderStatusDetailSimple
                    {
                        orderId = osd.OrderId,
                        statusName = osd.OrderStatus.OrderStatusName,
                        note = osd.OrderStatusNote,
                        updatedAt = osd.OrderStatusUpdatedAt
                    }).ToList(),
                    addressId = o.AddressId,
                    openRequest = o.OpenRequest,
                    refundRequest = o.RefundRequest,
                    paymentMethod = o.PaymentMethod,
                    totalPrice = o.TotalPrice,
                    orderItems = o.OrderItems.Select(oi => new OrderItemSimpleDto
                    {
                        orderItemId = oi.OrderItemId,
                        boxOptionId = oi.BoxOptionId,
                        quantity = oi.Quantity,
                        orderPrice = oi.OrderPrice,
                        isFeedback = oi.IsFeedback,
                        orderStatusCheckCardImage = oi.OrderStatusCheckCardImage,
                        isRefund = oi.IsRefund,
                        openRequest = oi.OpenRequest
                    }).ToList(),
                    currentStatusId = o.CurrentOrderStatusId
                }).FirstOrDefaultAsync();
        }
    }
}
