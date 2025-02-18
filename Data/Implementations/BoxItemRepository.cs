using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Implementations
{
    public class BoxItemRepository : IBoxItemRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public BoxItemRepository(BlindBoxSystemDbContext context)
        {
            _context = context;

        }

        public async Task<BoxItem> AddBoxItemAsync(BoxItem boxItem)
        {
            _context.BoxItems.Add(boxItem);
            await _context.SaveChangesAsync();
            return boxItem;
        }

        public async Task DeleteBoxItemAsync(int id)
        {
            var deletedBoxItem = await _context.BoxItems.FindAsync(id);
            if (deletedBoxItem != null)
            {
                _context.BoxItems.Remove(deletedBoxItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BoxItem>> GetAllBoxItemAsync()
        {
            return await _context.BoxItems.Include(b => b.Box).ToListAsync();
        }

        public async Task<BoxItem> GetBoxItemByIdAsync(int id)
        {
            return await _context.BoxItems.FindAsync(id);
        }

        public async Task<BoxItem> GetBoxItemByIdDTO(int id)
        {
            return await _context.BoxItems.Include(b => b.Box).FirstOrDefaultAsync(b => b.BoxItemId == id);
        }

        public async Task<BoxItem> UpdateBoxItemAsync(BoxItem boxItem)
        {
            _context.BoxItems.Update(boxItem);
            await _context.SaveChangesAsync();
            return boxItem;
        }
    }
}
