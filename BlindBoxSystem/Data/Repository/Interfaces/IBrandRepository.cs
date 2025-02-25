using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IBrandRepository
    {

        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        Task<Brand> GetBrandByIdAsync(int id);
        Task<Brand> GetBrandWithName(int id);
        Task<Brand> AddBrandAsync(Brand brand);

        Task<Brand> UpdateBrandAsync(Brand brand);

        Task DeleteBrandAsync(int id);

    }
}
