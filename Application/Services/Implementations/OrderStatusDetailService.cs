﻿using Application.Services.Interfaces;
using Common.Model.OrderStatusDetailDTOs;
using Data.Repository.Interfaces;

namespace Application.Services.Implementations
{
    public class OrderStatusDetailService : IOrderStatusDetailService
    {
        private readonly IOrderStatusDetailRepository _orderStatusDetailRepository;
        public OrderStatusDetailService(IOrderStatusDetailRepository orderStatusDetailRepository)
        {
            _orderStatusDetailRepository = orderStatusDetailRepository;
        }
        public async Task<bool> AddOrderStatusDetailAsync(OrderStatusDetailSimple orderStatusDetail)
        {

            return await _orderStatusDetailRepository.AddOrderStatusDetailAsync(orderStatusDetail);
        }

        public async Task<bool> CheckOrderStatusDetailExist(int orderId, int statusId)
        {
            return await _orderStatusDetailRepository.CheckOrderStatusDetailExist(orderId, statusId);
        }
    }
}
