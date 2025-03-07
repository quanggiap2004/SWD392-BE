using Application.Services.Interfaces;
using Common.Constants;
using Common.Exceptions;
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
            });
            return true;
        }
    }
}
