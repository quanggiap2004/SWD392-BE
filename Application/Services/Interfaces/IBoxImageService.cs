using Domain.Domain.Entities;
using Domain.Domain.Model.BoxImageDTOs;

namespace Application.Services.Interfaces
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
