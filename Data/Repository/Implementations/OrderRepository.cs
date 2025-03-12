using Common.Constants;
using Common.Model.Address.Response;
using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Common.Model.OrderItem;
using Common.Model.OrderStatusDetailDTOs;
using Common.Model.UserRolledItemDTOs;
using Common.Model.UserRolledItemDTOs.Response;
using Common.Model.VoucherDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
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
            var result = await _context.Orders.AsNoTracking()
                .Where(o => userId <= 0 || o.UserId == userId)
                .Select(o => new ManageOrderDto
                {
                    userId = o.UserId,
                    orderId = o.OrderId,
                    userName = o.User.Username,
                    orderCreatedAt = o.OrderCreatedAt,
                    orderStatusDetailsSimple = o.OrderStatusDetails.Select(osd => new OrderStatusDetailSimple
                    {
                        orderId = osd.OrderId,
                        statusName = osd.OrderStatus.OrderStatusName,
                        note = osd.OrderStatusNote,
                        updatedAt = osd.OrderStatusUpdatedAt
                    }).ToList(),
                    address = o.Address == null ? null : new AddressResponseDto
                    {
                        addressId = o.AddressId,
                        province = o.Address.Province,
                        district = o.Address.District,
                        ward = o.Address.Ward,
                        addressDetail = o.Address.AddressDetail,
                        userId = o.UserId,
                        name = o.Address.Name,
                        phoneNumber = o.Address.PhoneNumber
                    },
                    openRequest = o.OpenRequest,
                    refundRequest = o.RefundRequest,
                    paymentMethod = o.PaymentMethod,
                    totalPrice = o.TotalPrice,
                    currentStatusId = o.CurrentOrderStatusId,
                    shippingFee = o.ShippingFee,
                    voucher = o.Voucher == null ? null : new VoucherDto
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
                        openRequestNumber = oi.OpenRequestNumber,
                        boxOptionName = oi.BoxOption.BoxOptionName,
                        boxName = oi.BoxOption.Box.BoxName,
                        imageUrl = oi.BoxOption.Box.BoxImages.FirstOrDefault().BoxImageUrl,
                        numOfRefund = oi.NumOfRefund,
                        userRolledItemForManageOrder = oi.UserRolledItem == null ? null : new UserRolledItemForManageOrder
                        {
                            userRolledItemId = oi.UserRolledItem.UserRolledItemId,
                            boxItemImageUrl = oi.UserRolledItem.BoxItem.ImageUrl
                        }
                    }).ToList(),
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
                .Where(o => o.OrderId == orderId).Include(o => o.OrderItems).ThenInclude(oi => oi.UserRolledItem)
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
                        openRequestNumber = oi.OpenRequestNumber,
                        userRolledItemForManageOrder = oi.UserRolledItem == null ? null : new UserRolledItemForManageOrder
                        {
                            userRolledItemId = oi.UserRolledItem.UserRolledItemId,
                            boxItemImageUrl = oi.UserRolledItem.BoxItem.ImageUrl
                        }
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
                PaymentStatus = model.paymentStatus,
                ShippingFee = model.createOrderDto.shippingFee,
                DiscountAmount = model.createOrderDto.discountAmount,
                SubTotal = model.createOrderDto.subTotal
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
                currentOrderStatusId = order.CurrentOrderStatusId,
                shippingFee = model.createOrderDto.shippingFee,
                discountAmount = model.createOrderDto.discountAmount,
                subTotal = model.createOrderDto.subTotal
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
                IsEnable = false,
                ShippingFee = model.shippingFee,
                DiscountAmount = model.discountAmount,
                SubTotal = model.subTotal
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
            if (result != null)
            {
                result.PaymentStatus = createOrderDtoDetail.paymentStatus;
                result.Revenue = createOrderDtoDetail.revenue;
                result.OpenRequest = createOrderDtoDetail.openRequest;
                result.CurrentOrderStatusId = createOrderDtoDetail.currentOrderStatusId;
                result.IsEnable = true;
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
                    currentOrderStatusId = result.CurrentOrderStatusId,
                    shippingFee = result.ShippingFee
                };
            }
            return null;
        }
    }
}
