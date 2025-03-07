using Common.Model.BoxDTOs;
using Common.Model.BoxDTOs.ResponseDTOs;
using Domain.Domain.Entities;

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
        Task UpdateSoldQuantity(ICollection<OrderItem> orderItems);
        Task<BoxAndBoxItemResponseDto> getBoxByBoxOptionId(int boxOptionId);
    }
}
