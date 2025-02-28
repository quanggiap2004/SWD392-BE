using Application.Services.Interfaces;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Domain.Domain.Model.BrandDTOs;

namespace Application.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task<Brand> AddBrandAsync(Brand brand)
        {
            var addedBrand = await _brandRepository.AddBrandAsync(brand);
            return addedBrand;
        }

        public async Task DeleteBrandAsync(int id)
        {
            var existingBrand = await _brandRepository.GetBrandByIdAsync(id);
            if (existingBrand != null)
            {
                await _brandRepository.DeleteBrandAsync(id);
            }

        }

        public async Task<IEnumerable<GetAllBrandsDTO>> GetAllBrands()
        {

            var Brands = await _brandRepository.GetAllBrandsAsync();
            var BrandDTO = Brands.Select(b => new GetAllBrandsDTO
            {
                BrandId = b.BrandId,
                BrandName = b.BrandName,
                IncludedBox = b.Box.Select(n => n.BoxName).ToList(),
                imageUrl = b.ImageUrl
            });

            return BrandDTO;
        }

        public async Task<Brand> GetBrandById(int id)
        {
            var Brand = await _brandRepository.GetBrandByIdAsync(id);
            return Brand;
        }

        public async Task<GetAllBrandsDTO> GetBrandWithBoxName(int id)
        {
            var Brand = await _brandRepository.GetBrandWithName(id);
            var BrandDTO = new GetAllBrandsDTO
            {
                BrandId = Brand.BrandId,
                BrandName = Brand.BrandName,
                IncludedBox = Brand.Box.Select(b => b.BoxName).ToList(),
                imageUrl = Brand.ImageUrl
            };
            return BrandDTO;
        }

        public async Task<Brand> UpdateBrandAsync(int id, Brand brand)
        {
            var existingBrand = await _brandRepository.GetBrandByIdAsync(id);
            if (existingBrand == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBrand.BrandName = brand.BrandName;

            return await _brandRepository.UpdateBrandAsync(existingBrand);
        }
    }
}
