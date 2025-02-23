using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Application.Services.Implementations
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
    }
}
