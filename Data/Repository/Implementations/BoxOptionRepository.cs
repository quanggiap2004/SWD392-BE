using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Implementations
{
    public class BoxOptionRepository : IBoxOptionRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public BoxOptionRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<BoxOption> AddBoxOptionAsync(BoxOption boxOption)
        {
            _context.BoxOptions.Add(boxOption);
            await _context.SaveChangesAsync();
            return boxOption;
        }

        public async Task DeleteBoxOptionAsync(int id)
        {
            var deletedBoxOption = await _context.BoxOptions.FindAsync(id);
            if (deletedBoxOption != null)
            {
                _context.BoxOptions.Remove(deletedBoxOption);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BoxOption>> GetAllBoxOptionsAsync()
        {
            return await _context.BoxOptions.Include(b => b.Box).ToListAsync();
        }

        public async Task<BoxOption> GetBoxOptionByIdAsync(int id)
        {
            return await _context.BoxOptions.FindAsync(id);
        }

        public async Task<BoxOption> GetBoxOptionByIdDTO(int id)
        {
            return await _context.BoxOptions.Include(b => b.Box).FirstOrDefaultAsync(b => b.BoxOptionId == id);
        }

        public async Task<BoxOption> UpdateBoxOptionAsync(BoxOption boxOption)
        {
            _context.BoxOptions.Update(boxOption);
            await _context.SaveChangesAsync();
            return boxOption;
        }
    }
}
