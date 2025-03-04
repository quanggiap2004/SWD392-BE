using Domain.Domain.Entities;
using Domain.Domain.Model.BoxDTOs.ResponseDTOs;

namespace Data.Repository.Interfaces
{
    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> GetAllBoxesAsync();

        Task<Box> GetBoxByIdAsync(int id);
        Task<Box> GetBoxByIdDTO(int id);
        Task<Box> AddBoxAsync(Box Box);
        Task<Box> UpdateBoxAsync(Box Box);
        Task DeleteBoxAsync(int id);
        Task<IEnumerable<AllBoxesDto>> GetAllBox();
        Task<AllBoxesDto?> GetBoxByIdV2(int id);
        Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBox(int quantity);
        Task<IEnumerable<AllBoxesDto>> GetBoxByBrand(int brandId);

        Task<IEnumerable<Box>> SearchBoxesByNameAsync(string? boxName);
        Task<bool> UpdateSoldQuantity(ICollection<OrderItem> orderItems);
    }
}
