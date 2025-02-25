using Domain.Domain.Entities;
using Domain.Domain.Model.BoxOptionDTOs;
using Domain.Domain.Model.OrderItem;

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
