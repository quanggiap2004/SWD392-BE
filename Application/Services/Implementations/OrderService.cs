using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Common.Constants;
using BlindBoxSystem.Common.Exceptions;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;
using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;

namespace BlindBoxSystem.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusDetailService _orderStatusDetailService;
        private readonly IBoxOptionService _boxOption;
        public OrderService(IOrderRepository orderRepository, IOrderStatusDetailService orderStatusDetailService, IBoxOptionService boxOption)
        {
            _orderRepository = orderRepository;
            _orderStatusDetailService = orderStatusDetailService;
            _boxOption = boxOption;
        }

        public Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId)
        {
            return _orderRepository.GetAllOrders(userId);
        }


        public async Task<bool> CancelOrder(int orderId, string note)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if(order == null)
            {
                throw new CustomExceptions.NotFoundException("Order not found");
            }

            if(order.currentStatusId == (int)ProjectConstant.OrderStatus.Cancelled)
            {
                throw new CustomExceptions.BadRequestException("Order already cancelled");
            }

            //check order method
            if (order.paymentMethod.Equals(ProjectConstant.VnPay, StringComparison.OrdinalIgnoreCase))
            {
                //update order status
                var addStatus = await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
                {
                    orderId = orderId,
                    statusId = (int)ProjectConstant.OrderStatus.Cancelled,
                    updatedAt = DateTime.UtcNow,
                    note = note
                });
                if (!addStatus)
                {
                    throw new CustomExceptions.BadRequestException("Add order status failed");
                }

                var updateStatus = await _orderRepository.UpdateCurrentStatus(orderId, (int)ProjectConstant.OrderStatus.Cancelled);
                if(!updateStatus)
                {
                    throw new CustomExceptions.BadRequestException("Update order status failed");
                }


                var updateBoxoption = await _boxOption.UpdateStockQuantity(order.orderItems);
                if(!updateBoxoption)
                {
                    throw new CustomExceptions.BadRequestException("Update box option failed");
                }

                return true;
            }
            return false;
        }
    }
}
