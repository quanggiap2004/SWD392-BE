using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class BrandRepository : IBrandRepository
    {
        private readonly BlindBoxSystemDbContext _context;

        public BrandRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> AddBrandAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task DeleteBrandAsync(int id)
        {
            var deletedBrand = await _context.Brands.FindAsync(id);
            if (deletedBrand != null)
            {
                _context.Brands.Remove(deletedBrand);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.Include(b => b.Box).ToListAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<Brand> GetBrandWithBoxName(int id)
        {
            return await _context.Brands.Include(b => b.Box).FirstOrDefaultAsync(x => x.BrandId == id);
        }

        public async Task<Brand> UpdateBrandAsync(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return brand;
        }
    }

}
