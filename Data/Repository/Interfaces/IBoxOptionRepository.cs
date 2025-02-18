using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Interfaces
{
    public interface IBoxOptionRepository
    {
        Task<IEnumerable<BoxOption>> GetAllBoxOptionsAsync();

        Task<BoxOption> GetBoxOptionByIdAsync(int id);
        Task<BoxOption> GetBoxOptionByIdDTO(int id);
        Task<BoxOption> AddBoxOptionAsync(BoxOption boxOption);
        Task<BoxOption> UpdateBoxOptionAsync(BoxOption boxOption);
        Task DeleteBoxOptionAsync(int id);
    }
}
