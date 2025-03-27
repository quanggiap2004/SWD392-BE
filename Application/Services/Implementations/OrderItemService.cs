using Application.Services.Interfaces;
using Common.Constants;
using Common.Exceptions;
using Common.Model.OrderItem.Request;
using Common.Model.OrderStatusDetailDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;

namespace Application.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IBoxOptionService _boxOptionService;
        private readonly IOrderStatusDetailService _orderStatusDetailService;
        private readonly IBoxService _boxService;
        public OrderItemService(IOrderItemRepository orderItemRepository, IBoxOptionService boxOptionService, IOrderStatusDetailService orderStatusDetailService, IBoxService boxService)
        {
            _orderItemRepository = orderItemRepository;
            _boxOptionService = boxOptionService;
            _orderStatusDetailService = orderStatusDetailService;
            _boxService = boxService;
        }


        public async Task AddOrderItems(ICollection<OrderItem> orderItems)
        {
            await _boxOptionService.ReduceStockQuantity(orderItems);
            await _orderItemRepository.AddRangeOrderItems(orderItems);
            await _boxService.UpdateSoldQuantity(orderItems);
        }

        public async Task<OrderItem> GetOrderItemById(int orderItemId)
        {
            return await _orderItemRepository.GetOrderItemById(orderItemId);
        }

        public async Task<bool> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList)
        {
            var result = await _orderItemRepository.UpdateOpenBlindBoxForCustomerImage(orderItemId, imageList);
            if (result == null)
            {
                throw new CustomExceptions.NotFoundException("OrderItem not found");
            }
            await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = (int)ProjectConstant.OrderStatus.Shipping,
                note = "Staff uploaded image",
                updatedAt = DateTime.UtcNow,
            });
            return true;
        }

        public async Task<bool> UpdateDataAfterRefund(RefundOrderItemRequestDto request, int id)
        {
            var result = await _orderItemRepository.UpdateDataAfterRefund(request, id);
            if (!result)
            {
                throw new CustomExceptions.NotFoundException("OrderItem not found");
            }
            return true;
        }

        public async Task<bool> UpdateRefundRequest(int id)
        {
            var result = await _orderItemRepository.UpdateRefundRequest(id);
            if (!result)
            {
                throw new CustomExceptions.NotFoundException("OrderItem not found");
            }
            return true;
        }

        public async Task<bool> UpdateRefundDetails(int id, UpdateOrderItemRefundDetailsRequestDto request)
        {
            var orderItem = await _orderItemRepository.GetOrderItemById(id);
            if (orderItem == null)
            {
                throw new CustomExceptions.NotFoundException("OrderItem not found");
            }
            if(orderItem.RefundStatus != ProjectConstant.RefundRequest)
            {
                throw new CustomExceptions.BadRequestException("OrderItem not in Refund request status");
            }
            orderItem.Order.OrderUpdatedAt = DateTime.UtcNow;
            orderItem.NumOfRefund = request.numOfRefund;
            orderItem.Note = request.note == null ? "" : request.note;
            return await _orderItemRepository.SaveChangesAsync() > 0;
        }
    }
}
