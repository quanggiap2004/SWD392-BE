using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IBoxItemRepository
    {
        Task<IEnumerable<BoxItem>> GetAllBoxItemAsync();

        Task<BoxItem> GetBoxItemByIdAsync(int id);
        Task<BoxItem> GetBoxItemByIdDTO(int id);
        Task<BoxItem> AddBoxItemAsync(BoxItem boxItem);
        Task<BoxItem> UpdateBoxItemAsync(BoxItem boxItem);
        Task DeleteBoxItemAsync(int id);
    }
}
