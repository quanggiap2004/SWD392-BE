using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxImageDTOs;

namespace BlindBoxSystem.Application.Interfaces
{
    public interface IBoxImageService
    {
        Task<IEnumerable<GetAllBoxImageDTO>> GetAllBoxesImage();
        Task<BoxImage> GetBoxImageById(int id);
        Task<GetAllBoxImageDTO> GetBoxImageDTO(int id);
        Task<BoxImage> AddBoxImageAsync(BoxImage boxImage);
        Task<BoxImage> UpdateBoxImageAsync(int id, BoxImage boxImage);
        Task DeleteBoxImageAsync(int id);
    }
}
