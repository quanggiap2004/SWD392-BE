using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Implementations
{
    public class BoxRepository : IBoxRepository
    {
        private readonly BlindBoxSystemDbContext _context;

        public BoxRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Box> AddBoxAsync(Box Box)
        {
            _context.Boxes.Add(Box);
            await _context.SaveChangesAsync();
            return Box;
        }

        public async Task DeleteBoxAsync(int id)
        {
            var deletedBox = await _context.Boxes.FindAsync(id);
            if (deletedBox != null)
            {
                _context.Boxes.Remove(deletedBox);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Box>> GetAllBoxesAsync()
        {
            return await _context.Boxes.Include(b => b.Brand).Include(bImage => bImage.BoxImages).Include(bItem => bItem.BoxItems).Include(bOnline => bOnline.OnlineSerieBoxes).Include(bOption => bOption.BoxOptions).ToListAsync();
        }

        public async Task<Box> GetBoxByIdAsync(int id)
        {
            return await _context.Boxes.FindAsync(id);
        }

        public async Task<Box> GetBoxByIdDTO(int id)
        {
            return await _context.Boxes.Include(b => b.Brand).Include(bImage => bImage.BoxImages).Include(bItem => bItem.BoxItems).Include(bOnline => bOnline.OnlineSerieBoxes).Include(bOption => bOption.BoxOptions).FirstOrDefaultAsync(box => box.BoxId == id);
        }

        public async Task<Box> UpdateBoxAsync(Box Box)
        {
            _context.Boxes.Update(Box);
            await _context.SaveChangesAsync();
            return Box;
        }
    }
}
