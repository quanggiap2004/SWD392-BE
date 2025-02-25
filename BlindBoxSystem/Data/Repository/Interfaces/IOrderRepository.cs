﻿using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.OrderDTOs.Request;
using BlindBoxSystem.Domain.Model.OrderDTOs.Response;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderResponseDto> AddOrder(CreateOrderDtoDetail model);
        Task<ICollection<ManageOrderDto>> GetAllOrders(int? userId);
        Task<ManageOrderDto?> GetOrderById(int orderId);
        Task<DraftOrderDto> SaveDraftOrder(string jsonModel, CreateOrderDTO model);
        Task<bool> UpdateCurrentStatus(int orderId, int statusId);
        Task<Order?> GetOrderDto(int orderId);
        Task<OrderResponseDto> UpdateVnPayOrder(CreateOrderDtoDetail createOrderDtoDetail, int orderId);
    }
}
