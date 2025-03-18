using Application.Services.Interfaces;
using Common.Constants;
using Common.Exceptions;
using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Common.Model.OrderItem.Request;
using Common.Model.OrderStatusDetailDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Newtonsoft.Json;

namespace Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusDetailService _orderStatusDetailService;
        private readonly IBoxOptionService _boxOption;
        private readonly IOrderItemService _orderItemService;
        private readonly IVoucherService _voucherService;
        private readonly IBoxService _box;
        private readonly IUserRolledItemService _userRolledItemService;
        public OrderService(IOrderRepository orderRepository, IOrderStatusDetailService orderStatusDetailService, IBoxOptionService boxOption, IOrderItemService orderItemService, IVoucherService voucherService, IBoxService box, IUserRolledItemService userRolledItemService)
        {
            _orderRepository = orderRepository;
            _orderStatusDetailService = orderStatusDetailService;
            _boxOption = boxOption;
            _orderItemService = orderItemService;
            _voucherService = voucherService;
            _box = box;
            _userRolledItemService = userRolledItemService;
        }

        public Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId)
        {
            return _orderRepository.GetAllOrders(userId);
        }


        public async Task<bool> CancelOrder(int orderId, string note)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new CustomExceptions.NotFoundException("Order not found");
            }

            if (order.currentStatusId == (int)ProjectConstant.OrderStatus.Cancelled)
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
                if (!updateStatus)
                {
                    throw new CustomExceptions.BadRequestException("Update order status failed");
                }


                var updateBoxoption = await _boxOption.UpdateStockQuantity(order.orderItems);
                if (!updateBoxoption)
                {
                    throw new CustomExceptions.BadRequestException("Update box option failed");
                }

                ICollection<OrderItem> orderItems = order.orderItems.Select(model => new OrderItem
                {
                    OrderItemId = model.orderItemId,
                    OrderStatusCheckCardImage = new List<string>(),
                    BoxOptionId = model.boxOptionId,
                }).ToList();

                List<int> currentUserRolledItemIds = new List<int>();
                foreach (var item in order.orderItems)
                {
                    if(item.userRolledItemForManageOrder != null)
                    {
                        currentUserRolledItemIds.Add(item.userRolledItemForManageOrder.userRolledItemId);
                    }
                }
                var updateResult = await _userRolledItemService.UpdateUserRolledItemCheckoutStatus(currentUserRolledItemIds, false);
                if(!updateResult)
                {
                    throw new CustomExceptions.BadRequestException("Update user rolled item failed");
                }
                await _box.UpdateSoldQuantity(orderItems);
                
                return true;
            }
            return false;
        }

        public async Task<OrderResponseDto> CreateOrderCOD(CreateOrderDTO model)
        {
            decimal revenue = CalculateRevenue(model);
            bool openRequest = false;
            int currentOrderStatusId = 1; //Processing with VnPay
            string paymentStatus = ProjectConstant.PaymentPending;
            OrderResponseDto result = new OrderResponseDto();
            foreach (var item in model.orderItemRequestDto)
            {
                if (item.orderItemOpenRequestNumber > 0)
                {
                    throw new CustomExceptions.BadRequestException("Open request not allowed for COD");
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
                OpenRequestNumber = model.orderItemOpenRequestNumber,
                UserRolledItemId = model.userRolledItemId,
            }).ToList();
            await _orderItemService.AddOrderItems(orderItems); //subtracts stock quantity
            await UpdateUserRolledItemCheckoutStatus(model.orderItemRequestDto, true);
            await _voucherService.ReduceVoucherQuantity(model.voucherId);
            return result;
        }

        private decimal CalculateRevenue(CreateOrderDTO model)
        {
            decimal originPrice = model.orderItemRequestDto.Select(m => m.quantity * m.originPrice).Sum();
            return model.totalPrice - model.shippingFee - originPrice;
        }

        public async Task<DraftOrderDto> SaveDraftOrder(CreateOrderDTO model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            return await _orderRepository.SaveDraftOrder(jsonModel, model);
        }

        public async Task<CreateOrderDTO> GetOrderDto(int orderId)
        {
            var result = await _orderRepository.GetOrderDto(orderId);
            if (result == null)
            {
                throw new CustomExceptions.NotFoundException("Order not found");
            }
            CreateOrderDTO orderDto = JsonConvert.DeserializeObject<CreateOrderDTO>(result.JsonOrderModel);
            return orderDto;
        }

        public async Task<OrderResponseDto> UpdateOrderVnPay(CreateOrderDTO model, int orderId)
        {
            decimal revenue = CalculateRevenue(model);
            bool openRequest = false;
            int currentOrderStatusId = 2; //Processing with VnPay
            string paymentStatus = ProjectConstant.PaymentSuccess;
            bool onlineSerieBoxCheck = false;
            foreach (var item in model.orderItemRequestDto)
            {
                if (item.isOnlineSerieBox && item.price > 0) //check if this is online serie box
                {
                    currentOrderStatusId = (int)ProjectConstant.OrderStatus.Arrived;
                    onlineSerieBoxCheck = true;
                    break;
                }
                if (item.orderItemOpenRequestNumber > 0)
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
                currentOrderStatusId = currentOrderStatusId,
                createOrderDto = model,
            }, orderId);

            bool addOrderStatus = AddOrderStatus(onlineSerieBoxCheck, result, currentOrderStatusId);

            ICollection<OrderItem> orderItems = model.orderItemRequestDto.Select(model => new OrderItem
            {
                OrderId = result.orderId,
                BoxOptionId = model.boxOptionId,
                Quantity = model.quantity,
                OrderPrice = model.price,
                OpenRequestNumber = model.orderItemOpenRequestNumber,
                OrderStatusCheckCardImage = new List<string>(),
                UserRolledItemId = model.userRolledItemId
            }).ToList();
            await _orderItemService.AddOrderItems(orderItems);
            await UpdateUserRolledItemCheckoutStatus(model.orderItemRequestDto, true);
            await _voucherService.ReduceVoucherQuantity(model.voucherId);
            return result;
        }

        private bool AddOrderStatus(bool onlineSerieBoxCheck, OrderResponseDto result, int currentOrderStatusId)
        {
            if (onlineSerieBoxCheck)
            {
                return _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
                {
                    orderId = result.orderId,
                    statusId = (int)ProjectConstant.OrderStatus.Arrived,
                    note = "Order arrived",
                }).Result;
            }
            return _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = currentOrderStatusId,
                note = "Order created",
            }).Result;
        }
         
        private async Task<bool> UpdateUserRolledItemCheckoutStatus(ICollection<OrderItemRequestDto> orderItemRequestDto, bool status)
        {
            List<int> currentUserRolledItemIds = new List<int>();
            foreach (var item in orderItemRequestDto)
            {
                if (item.isOnlineSerieBox)
                {
                    currentUserRolledItemIds.Add(item.userRolledItemId.Value);
                }
            }
            return await _userRolledItemService.UpdateUserRolledItemCheckoutStatus(currentUserRolledItemIds, status);
        }

        public async Task<ManageOrderDto> GetOrderById(int orderId)
        {
            var result = await _orderRepository.GetOrderById(orderId);
            if(result == null)
            {
                throw new CustomExceptions.NotFoundException("Order not found: " + orderId);
            }
            return result;
        }
    }
}
