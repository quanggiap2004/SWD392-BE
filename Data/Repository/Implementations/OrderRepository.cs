using BlindBoxSystem.Common.Constants;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;
using BlindBoxSystem.Domain.Model.OrderItem;
using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;
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

        public async Task<OrderResponseDto> AddOrder(CreateOrderDtoDetail model)
        {
            var order = new Order
            {
                UserId = model.createOrderDto.userId,
                OrderCreatedAt = DateTime.UtcNow,
                VoucherId = model.createOrderDto.voucherId,
                PaymentMethod = model.createOrderDto.paymentMethod,
                TotalPrice = model.createOrderDto.totalPrice,
                Revenue = model.revenue,
                AddressId = model.createOrderDto.addressId,
                OpenRequest = model.openRequest,
                CurrentOrderStatusId = model.currentOrderStatusId,
                PaymentStatus = model.paymentStatus
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return new OrderResponseDto
            {
                orderId = order.OrderId,
                userId = order.UserId,
                orderCreatedAt = order.OrderCreatedAt,
                paymentMethod = order.PaymentMethod,
                totalPrice = order.TotalPrice,
                addressId = order.AddressId,
                revenue = order.Revenue,
                currentOrderStatusId = order.CurrentOrderStatusId
            };
        }

        public async Task<DraftOrderDto> SaveDraftOrder(string jsonModel, CreateOrderDTO model)
        {
            var order = new Order
            {
                JsonOrderModel = jsonModel,
                UserId = model.userId,
                OrderCreatedAt = DateTime.UtcNow,
                VoucherId = model.voucherId,
                PaymentMethod = model.paymentMethod,
                TotalPrice = model.totalPrice,
                Revenue = 0,
                AddressId = model.addressId,
                OpenRequest = false,
                CurrentOrderStatusId = 1,
                PaymentStatus = ProjectConstant.PaymentPending,
                IsEnable = false
            };
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return new DraftOrderDto
            {
                jsonOrder = jsonModel,
                orderId = order.OrderId
            };
        }

        public async Task<Order?> GetOrderDto(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<OrderResponseDto> UpdateVnPayOrder(CreateOrderDtoDetail createOrderDtoDetail, int orderId)
        {
            var result = await _context.Orders.FindAsync(orderId);
            if(result != null)
            {
                result.PaymentStatus = createOrderDtoDetail.paymentStatus;
                result.Revenue = createOrderDtoDetail.revenue;
                result.OpenRequest = createOrderDtoDetail.openRequest;
                result.CurrentOrderStatusId = createOrderDtoDetail.currentOrderStatusId;
                await _context.SaveChangesAsync();
                return new OrderResponseDto
                {
                    orderId = result.OrderId,
                    userId = result.UserId,
                    orderCreatedAt = result.OrderCreatedAt,
                    paymentMethod = result.PaymentMethod,
                    totalPrice = result.TotalPrice,
                    addressId = result.AddressId,
                    revenue = result.Revenue,
                    currentOrderStatusId = result.CurrentOrderStatusId
                };
            }
            return null;
        }
    }
}
