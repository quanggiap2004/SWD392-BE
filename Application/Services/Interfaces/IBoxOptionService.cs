using Common.Model.BoxOptionDTOs;
using Common.Model.OrderItem;
using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IBoxOptionService
    {
        Task<IEnumerable<GetAllBoxOptionDTO>> GetAllBoxOptions();
        Task<BoxOption> GetBoxOptionById(int id);
        Task<GetAllBoxOptionDTO> GetBoxOptionDTO(int id);
        Task<BoxOption> AddBoxOptionAsync(BoxOption boxOption);
        Task<BoxOption> UpdateBoxOptionAsync(int id, BoxOption boxOption);
        Task DeleteBoxOptionAsync(int id);
        Task<bool> UpdateStockQuantity(ICollection<OrderItemSimpleDto> orderItems);
        Task ReduceStockQuantity(ICollection<OrderItem> orderItems);
    }
}
