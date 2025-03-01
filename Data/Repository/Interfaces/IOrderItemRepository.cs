using Domain.Domain.Entities;
using Domain.Domain.Model.OrderItem.Response;

namespace Data.Repository.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddRangeOrderItems(ICollection<OrderItem> orderItems);
        Task<OpenRequestResponseDto?> UpdateOpenBlindBoxForCustomerImage(int orderItemId, List<string> imageList);
    }
}
