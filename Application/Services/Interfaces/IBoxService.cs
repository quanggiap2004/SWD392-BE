using Domain.Domain.Entities;
using Domain.Domain.Model.BoxDTOs;
using Domain.Domain.Model.BoxDTOs.ResponseDTOs;

namespace Application.Services.Interfaces
{
    public interface IBoxService
    {
        Task<IEnumerable<GetAllBoxesDTO>> GetAllBoxes();
        Task<IEnumerable<AllBoxesDto>> GetAllBox();
        Task<Box> GetBoxById(int id);
        Task<GetAllBoxesDTO> GetBoxByIdDTO(int id);
        Task<Box> AddBoxAsync(Box box);
        Task<Box> UpdateBoxAsync(int id, Box box);
        Task DeleteBoxAsync(int id);
        Task<AllBoxesDto> GetBoxByIdV2(int id);
        Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBox(int quantity);
        Task<IEnumerable<AllBoxesDto>> GetBoxByBrand(int brandId);
        Task<IEnumerable<GetAllBoxesDTO>> SearchBoxesByNameAsync(string? boxName);

    }
}
