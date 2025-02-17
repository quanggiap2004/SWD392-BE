using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Interfaces
{
    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> GetAllBoxesAsync();

        Task<Box> GetBoxByIdAsync(int id);
        Task<Box> GetBoxByIdDTO(int id);
        Task<Box> AddBoxAsync(Box Box);
        Task<Box> UpdateBoxAsync(Box Box);
        Task DeleteBoxAsync(int id);
    }
}
