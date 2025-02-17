using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BrandDTOs;

namespace BlindBoxSystem.Application.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<GetAllBrandsDTO>> GetAllBrands();
        Task<Brand> GetBrandById(int id);
        Task<GetAllBrandsDTO> GetBrandWithBoxName(int id);
        Task<Brand> AddBrandAsync(Brand brand);
        Task<Brand> UpdateBrandAsync(int id, Brand brand);
        Task DeleteBrandAsync(int id);

    }
}
