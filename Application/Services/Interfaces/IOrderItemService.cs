using Common.Model.OrderItem.Request;
using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItems(ICollection<OrderItem> orderItems);
        Task<bool> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList);
        Task<OrderItem> GetOrderItemById(int orderItemId);
        Task<bool> UpdateDataAfterRefund(RefundOrderItemRequestDto request, int id);
        Task<bool> UpdateRefundRequest(int id);
    }
}
