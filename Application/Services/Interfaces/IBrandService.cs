using Domain.Domain.Entities;
using Domain.Domain.Model.BrandDTOs;

namespace Application.Services.Interfaces
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
