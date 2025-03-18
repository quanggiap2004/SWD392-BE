using Common.Model.OrderItem;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IBoxOptionRepository
    {
        Task<IEnumerable<BoxOption>> GetAllBoxOptionsAsync();

        Task<BoxOption> GetBoxOptionByIdAsync(int id);
        Task<BoxOption> GetBoxOptionByIdDTO(int id);
        Task<BoxOption> AddBoxOptionAsync(BoxOption boxOption);
        Task<BoxOption> UpdateBoxOptionAsync(BoxOption boxOption);
        Task DeleteBoxOptionAsync(int id);
        Task<bool> UpdateStockQuantity(ICollection<OrderItemSimpleDto> orderItems);
        Task ReduceStockQuantity(ICollection<OrderItem> orderItems);
        Task<BoxOption> GetBoxOptionByIdWithOnlineSerieBox(int id);
        Task<bool> UpdateAverageBoxOptionRating(int boxOptionId);
    }
}
