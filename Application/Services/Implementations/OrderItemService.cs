using Application.Services.Interfaces;
using Common.Exceptions;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;

namespace Application.Services.Implementations
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IBoxOptionService _boxOptionService;
        public OrderItemService(IOrderItemRepository orderItemRepository, IBoxOptionService boxOptionService)
        {
            _orderItemRepository = orderItemRepository;
            _boxOptionService = boxOptionService;
        }


        public async Task AddOrderItems(ICollection<OrderItem> orderItems)
        {
            await _boxOptionService.ReduceStockQuantity(orderItems);
            await _orderItemRepository.AddRangeOrderItems(orderItems);
        }

        public async Task<bool> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList)
        {
           var result = await _orderItemRepository.UpdateOpenBlindBoxForCustomerImage(orderItemId, imageList);
            if (result == false)
            {
                throw new CustomExceptions.NotFoundException("OrderItem not found");
            }
            return result;
        }
    }
}
