using Domain.Domain.Entities;
using Domain.Domain.Model.BoxItemDTOs;

namespace Application.Services.Interfaces
{
    public interface IBoxItemService
    {
        Task<IEnumerable<GetAllBoxItemDTO>> GetAllBoxItems();
        Task<BoxItem> GetBoxItemById(int id);
        Task<GetAllBoxItemDTO> GetBoxItemDTO(int id);
        Task<BoxItem> AddBoxItemAsync(BoxItem boxItem);
        Task<BoxItem> UpdateBoxItemAsync(int id, BoxItem boxItem);
        Task DeleteBoxItemAsync(int id);
    }
}
