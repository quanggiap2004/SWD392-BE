using Application.Services.Interfaces;
using Common.Constants;
using Common.Exceptions;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Domain.Domain.Model.OrderStatusDetailDTOs;

namespace Application.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IBoxOptionService _boxOptionService;
        private readonly IOrderStatusDetailService _orderStatusDetailService;
        public OrderItemService(IOrderItemRepository orderItemRepository, IBoxOptionService boxOptionService, IOrderStatusDetailService orderStatusDetailService)
        {
            _orderItemRepository = orderItemRepository;
            _boxOptionService = boxOptionService;
            _orderStatusDetailService = orderStatusDetailService;
        }


        public async Task AddOrderItems(ICollection<OrderItem> orderItems)
        {
            await _boxOptionService.ReduceStockQuantity(orderItems);
            await _orderItemRepository.AddRangeOrderItems(orderItems);
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
            });
            return true;
        }
    }
}
