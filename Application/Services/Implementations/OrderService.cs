using Application.Services.Interfaces;
using AutoMapper;
using Common.Constants;
using Common.Exceptions;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.OnlineSerieBoxDTOs.Request;
using Common.Model.OrderDTOs.Request;
using Common.Model.OrderDTOs.Response;
using Common.Model.OrderItem.Request;
using Common.Model.OrderStatusDetailDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IOnlineSerieBoxService _onlineSerieBoxService;
        public OrderService(IOrderRepository orderRepository, IOrderStatusDetailService orderStatusDetailService, IBoxOptionService boxOption, 
            IOrderItemService orderItemService, IVoucherService voucherService, IBoxService box, IUserRolledItemService userRolledItemService, IOnlineSerieBoxService onlineSerieBoxService)
        {
            _orderRepository = orderRepository;
            _orderStatusDetailService = orderStatusDetailService;
            _boxOption = boxOption;
            _orderItemService = orderItemService;
            _voucherService = voucherService;
            _box = box;
            _userRolledItemService = userRolledItemService;
            _onlineSerieBoxService = onlineSerieBoxService;
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
                if(currentUserRolledItemIds.Count > 0)
                {
                    var updateResult = await _userRolledItemService.UpdateUserRolledItemCheckoutStatus(currentUserRolledItemIds, false);
                    if (!updateResult)
                    {
                        throw new CustomExceptions.BadRequestException("Update user rolled item failed");
                    }
                }
                await _box.UpdateSoldQuantity(orderItems);
                
                return true;
            }
            return false;
        }

        public async Task<OrderResponseDto> CreateOrderCOD(CreateOrderDTO model)
        {
            if(model.isShipBoxItem)
            {
                throw new CustomExceptions.BadRequestException("COD not allowed for ship box item");
            }
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
                updatedAt = DateTime.UtcNow
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
            if (model.orderId != null || model.orderId > 0)
            {
                await UpdateShippingFeeAndAddress(model.orderId.Value, model.shippingFee, model.addressId.Value);
                return new DraftOrderDto
                {
                    jsonOrder = "",
                    orderId = model.orderId.Value
                };
            }
            var jsonModel = JsonConvert.SerializeObject(model);
            return await _orderRepository.SaveDraftOrder(jsonModel, model);
        }

        public async Task UpdateShippingFeeAndAddress(int orderId, decimal shippingFee, int address)
        {
            await _orderRepository.UpdateShippingFeeAndAddress(orderId, shippingFee, address);
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
            int currentOrderStatusId = (int)ProjectConstant.OrderStatus.Processing; //Processing with VnPay
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

            bool addOrderPendingStatus = await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = (int)ProjectConstant.OrderStatus.Pending,
                note = "Order created",
                updatedAt = DateTime.UtcNow
            });
            bool addOrderStatus = await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = currentOrderStatusId,
                note = "Order processing",
                updatedAt = DateTime.UtcNow
            });

            ICollection<OrderItem> orderItems = model.orderItemRequestDto.Select(model => new OrderItem
            {
                OrderId = result.orderId,
                BoxOptionId = model.boxOptionId,
                Quantity = model.quantity,
                OrderPrice = model.price,
                OpenRequestNumber = model.orderItemOpenRequestNumber,
                OrderStatusCheckCardImage = new List<string>(),
                UserRolledItemId = model.userRolledItemId,
                RefundStatus = model.isOnlineSerieBox ? ProjectConstant.RefundResolved : ProjectConstant.RefundAvailable,
            }).ToList();
            await _orderItemService.AddOrderItems(orderItems);
            await UpdateUserRolledItemCheckoutStatus(model.orderItemRequestDto, true);
            await _voucherService.ReduceVoucherQuantity(model.voucherId);
            return result;
        }

        //private bool AddOrderStatus(bool onlineSerieBoxCheck, OrderResponseDto result, int currentOrderStatusId)
        //{
        //    if (onlineSerieBoxCheck)
        //    {
        //        return _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
        //        {
        //            orderId = result.orderId,
        //            statusId = (int)ProjectConstant.OrderStatus.Arrived,
        //            note = "Order arrived",
        //            updatedAt = DateTime.UtcNow
        //        }).Result;
        //    }
        //    return _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
        //    {
        //        orderId = result.orderId,
        //        statusId = currentOrderStatusId,
        //        note = "Order created",
        //        updatedAt = DateTime.UtcNow
        //    }).Result;
        //}
         
        private async Task<bool> UpdateUserRolledItemCheckoutStatus(ICollection<OrderItemRequestDto> orderItemRequestDto, bool status)
        {
            List<int> currentUserRolledItemIds = new List<int>();
            foreach (var item in orderItemRequestDto)
            {
                if (item.isOnlineSerieBox && item.userRolledItemId != null)
                {
                    currentUserRolledItemIds.Add(item.userRolledItemId.Value);
                }
            }
            if(currentUserRolledItemIds.Count <= 0)
            {
                return false;
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

        public async Task<BoxItemPaymentResponseDto> ProcessOnlineSerieBoxOrder(CreateOrderDTO model, int orderId)
        {
            decimal revenue = CalculateRevenue(model);
            bool openRequest = false;
            int currentOrderStatusId = (int)ProjectConstant.OrderStatus.Processing;
            string paymentStatus = ProjectConstant.PaymentSuccess;
            bool onlineSerieBoxCheck = true;
            var boxItemResponse = await _onlineSerieBoxService.OpenOnlineSerieBoxAsync(new OpenOnlineSerieBoxRequest
            {
                userId = model.userId,
                onlineSerieBoxId = model.orderItemRequestDto.First().boxOptionId,
            });
            OrderResponseDto result = await _orderRepository.UpdateVnPayOrder(new CreateOrderDtoDetail
            {
                paymentStatus = paymentStatus,
                revenue = revenue,
                openRequest = openRequest,
                currentOrderStatusId = currentOrderStatusId,
                createOrderDto = model,
            }, orderId);
            await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = (int)ProjectConstant.OrderStatus.Pending,
                note = "Order created",
                updatedAt = DateTime.UtcNow
            });
            await _orderStatusDetailService.AddOrderStatusDetailAsync(new OrderStatusDetailSimple
            {
                orderId = result.orderId,
                statusId = (int)ProjectConstant.OrderStatus.Processing,
                note = "Waiting for shipping box item",
                updatedAt = DateTime.UtcNow
            });
            ICollection<OrderItem> orderItems = model.orderItemRequestDto.Select(model => new OrderItem
            {
                OrderId = result.orderId,
                BoxOptionId = model.boxOptionId,
                Quantity = model.quantity,
                OrderPrice = model.price,
                OpenRequestNumber = model.orderItemOpenRequestNumber,
                OrderStatusCheckCardImage = new List<string>(),
                UserRolledItemId = boxItemResponse.userRolledItem.userRolledItemId,
                RefundStatus = ProjectConstant.RefundAvailable
            }).ToList();
            await _orderItemService.AddOrderItems(orderItems);         
            
            return new BoxItemPaymentResponseDto {
                boxItemResponseDto = boxItemResponse,
                boxOptionId = model.orderItemRequestDto.First().boxOptionId,
            };
        }

        public async Task<bool> UpdateOnlineSerieBoxTotalPrice(int orderId)
        {
            return await _orderRepository.UpdateOnlineSerieBoxAfterShip(orderId);
        }

        public async Task<bool> UpdateOrderForShipping(int orderId, int status)
        {
            var order = await _orderRepository.GetOrderEntityById(orderId);
            if (order == null)
            {
                throw new CustomExceptions.NotFoundException("order is invalid or current status is not in processing or shipping state");
            }
            order.CurrentOrderStatusId = status;
            order.PaymentStatus = ProjectConstant.PaymentSuccess;
            return await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateOrderPendingStatus()
        {
            await _orderRepository.UpdateCodOrdersPendingStatus();
            return true;
        }
    }
}
