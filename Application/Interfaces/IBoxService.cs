using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxDTOs;

namespace BlindBoxSystem.Application.Interfaces
{
    public interface IBoxService
    {
        Task<IEnumerable<GetAllBoxesDTO>> GetAllBoxes();
        Task<Box> GetBoxById(int id);
        Task<GetAllBoxesDTO> GetBoxByIdDTO(int id);
        Task<Box> AddBoxAsync(Box box);
        Task<Box> UpdateBoxAsync(int id, Box box);
        Task DeleteBoxAsync(int id);
    }
}
