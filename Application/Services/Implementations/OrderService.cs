using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Common.Constants;
using BlindBoxSystem.Common.Exceptions;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;
using BlindBoxSystem.Domain.Model.OrderStatusDetailDTOs;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace BlindBoxSystem.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusDetailService _orderStatusDetailService;
        private readonly IBoxOptionService _boxOption;
        private readonly IOrderItemService _orderItemService;
        private readonly IVoucherService _voucherService;
        public OrderService(IOrderRepository orderRepository, IOrderStatusDetailService orderStatusDetailService, IBoxOptionService boxOption, IOrderItemService orderItemService, IVoucherService voucherService)
        {
            _orderRepository = orderRepository;
            _orderStatusDetailService = orderStatusDetailService;
            _boxOption = boxOption;
            _orderItemService = orderItemService;
            _voucherService = voucherService;
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
            if (order.paymentMethod.Equals(ProjectConstant.COD, StringComparison.OrdinalIgnoreCase))
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

        public async Task<OrderResponseDto> CreateOrderCOD(CreateOrderDTO model)
        {
            float revenue = CalculateRevenue(model);
            bool openRequest = false;
            int currentOrderStatusId = 1; //Processing with VnPay
            string paymentStatus = ProjectConstant.PaymentPending;
            OrderResponseDto result = new OrderResponseDto();
            foreach (var item in model.orderItemRequestDto)
            {
                if(item.orderItemOpenRequest == true)
                {
                    openRequest= true;
                    break;
                }
            }

            result = await _orderRepository.AddOrder(new CreateOrderDtoDetail
            {
                paymentStatus = paymentStatus,
                revenue = revenue,
                openRequest = openRequest,
                currentOrderStatusId = currentOrderStatusId,
                createOrderDto = model,
            });
            await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = (int)ProjectConstant.OrderStatus.Pending,
                note = "Order created",
            });

            ICollection<OrderItem> orderItems = model.orderItemRequestDto.Select(model => new OrderItem
            {
                OrderId = result.orderId,
                BoxOptionId = model.boxOptionId,
                Quantity = model.quantity,
                OrderPrice = model.price,
                OpenRequest = model.orderItemOpenRequest,
            }).ToList();
            await _orderItemService.AddOrderItems(orderItems); //subtracts stock quantity
            await _voucherService.ReduceVoucherQuantity(model.voucherId);
            return result;
        }

        private float CalculateRevenue(CreateOrderDTO model)
        {
            return model.orderItemRequestDto.Select(m => m.quantity * (m.price - m.originPrice)).Sum();
        }

        public async Task<DraftOrderDto> SaveDraftOrder(CreateOrderDTO model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            return await _orderRepository.SaveDraftOrder(jsonModel, model);
        }

        public async Task<CreateOrderDTO> GetOrderDto(int orderId)
        {
            var result = await _orderRepository.GetOrderDto(orderId);
            if(result == null)
            {
                throw new CustomExceptions.NotFoundException("Order not found");
            }
            CreateOrderDTO orderDto = JsonConvert.DeserializeObject<CreateOrderDTO>(result.JsonOrderModel);
            return orderDto;
        }

        public async Task<OrderResponseDto> UpdateOrderVnPay(CreateOrderDTO model, int orderId)
        {
            float revenue = CalculateRevenue(model);
            bool openRequest = false;
            int currentOrderStatusId = 2; //Processing with VnPay
            string paymentStatus = ProjectConstant.PaymentSuccess;
            foreach (var item in model.orderItemRequestDto)
            {
                if (item.orderItemOpenRequest == true)
                {
                    openRequest = true;
                    break;
                }
            }

            OrderResponseDto result = await _orderRepository.UpdateVnPayOrder(new CreateOrderDtoDetail
                {
                    paymentStatus = paymentStatus,
                    revenue = revenue,
                    openRequest = openRequest,
                    currentOrderStatusId = (int)ProjectConstant.OrderStatus.Processing,
                    createOrderDto = model,
                }, orderId);

            await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = (int)ProjectConstant.OrderStatus.Processing,
                note = "Order created",
            });

            ICollection<OrderItem> orderItems = model.orderItemRequestDto.Select(model => new OrderItem
            {
                OrderId = result.orderId,
                BoxOptionId = model.boxOptionId,
                Quantity = model.quantity,
                OrderPrice = model.price,
                OpenRequest = model.orderItemOpenRequest,
            }).ToList();
            await _orderItemService.AddOrderItems(orderItems); //subtracts stock quantity
            await _voucherService.ReduceVoucherQuantity(model.voucherId);
            return result;
        }
    }
}
