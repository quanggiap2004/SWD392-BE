using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Repository.Implementations
{
    public class BoxImageRepository : IBoxImageRepository
    {

        private readonly BlindBoxSystemDbContext _context;
        public BoxImageRepository(BlindBoxSystemDbContext context)
        {
            _context = context;

        }
        public async Task<BoxImage> AddBoxImageAsync(BoxImage boxImage)
        {
            _context.BoxImages.Add(boxImage);
            await _context.SaveChangesAsync();
            return boxImage;
        }

        public async Task DeleteBoxImageAsync(int id)
        {
            var deletedBoxImage = await _context.BoxImages.FindAsync(id);
            if (deletedBoxImage != null)
            {
                _context.BoxImages.Remove(deletedBoxImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BoxImage>> GetAllBoxImageAsync()
        {
            return await _context.BoxImages.Include(b => b.Box).ToListAsync();
        }

        public async Task<BoxImage> GetBoxImageByIdAsync(int id)
        {
            return await _context.BoxImages.FindAsync(id);
        }

        public async Task<BoxImage> GetBoxImageByIdDTO(int id)
        {
            return await _context.BoxImages.Include(b => b.Box).FirstOrDefaultAsync(b => b.BoxImageId == id);
        }

        public async Task<BoxImage> UpdateBoxImageAsync(BoxImage boxImage)
        {
            _context.BoxImages.Update(boxImage);
            await _context.SaveChangesAsync();
            return boxImage;
        }
    }
}
