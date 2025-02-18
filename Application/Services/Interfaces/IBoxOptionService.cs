using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxOptionDTOs;

namespace BlindBoxSystem.Application.Interfaces
{
    public interface IBoxOptionService
    {
        Task<IEnumerable<GetAllBoxOptionDTO>> GetAllBoxOptions();
        Task<BoxOption> GetBoxOptionById(int id);
        Task<GetAllBoxOptionDTO> GetBoxOptionDTO(int id);
        Task<BoxOption> AddBoxOptionAsync(BoxOption boxOption);
        Task<BoxOption> UpdateBoxOptionAsync(int id, BoxOption boxOption);
        Task DeleteBoxOptionAsync(int id);
    }
}
