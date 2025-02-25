using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IBoxImageRepository
    {
        Task<IEnumerable<BoxImage>> GetAllBoxImageAsync();

        Task<BoxImage> GetBoxImageByIdAsync(int id);
        Task<BoxImage> GetBoxImageByIdDTO(int id);
        Task<BoxImage> AddBoxImageAsync(BoxImage boxImage);
        Task<BoxImage> UpdateBoxImageAsync(BoxImage boxImage);
        Task DeleteBoxImageAsync(int id);
    }
}
