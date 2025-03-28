using Common.Model.OrderItem;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
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
                deletedBoxOption.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BoxOption>> GetAllBoxOptionsAsync()
        {
            return await _context.BoxOptions.Include(b => b.Box).Where(b => b.IsDeleted == false).ToListAsync();
        }

        public async Task<BoxOption> GetBoxOptionByIdAsync(int id)
        {
            return await _context.BoxOptions.FindAsync(id);
        }

        public async Task<BoxOption> GetBoxOptionByIdWithOnlineSerieBox(int id)
        {
            return await _context.BoxOptions.Include(b => b.OnlineSerieBox).FirstOrDefaultAsync(bo => bo.BoxOptionId == id);
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

        public async Task<bool> UpdateStockQuantity(ICollection<OrderItemSimpleDto> orderItems)
        {
            var boxOptions = await _context.BoxOptions.Where(b => orderItems.Select(oi => oi.boxOptionId).Contains(b.BoxOptionId)).ToListAsync();
            foreach (var orderItem in orderItems)
            {
                var boxOption = boxOptions.FirstOrDefault(b => b.BoxOptionId == orderItem.boxOptionId);
                if (boxOption != null)
                {
                    boxOption.BoxOptionStock += orderItem.quantity;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ReduceStockQuantity(ICollection<OrderItem> orderItems)
        {
            var boxOptionIds = orderItems.Select(oi => oi.BoxOptionId).ToList();
            var boxOptionList = await _context.BoxOptions.Where(b => boxOptionIds.Contains(b.BoxOptionId)).ToListAsync();
            foreach (var boxOption in boxOptionList)
            {
                if (boxOption.IsOnlineSerieBox == false)
                {
                    boxOption.BoxOptionStock -= orderItems.FirstOrDefault(oi => oi.BoxOptionId == boxOption.BoxOptionId).Quantity;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAverageBoxOptionRating(int boxOptionId)
        {
            var orderItems = await _context.OrderItems.Where(x => x.BoxOptionId == boxOptionId && x.Feedback != null).Include(x => x.Feedback).ToListAsync();
            if (orderItems.Count == 0)
            {
                return false;
            }
            float totalRating = 0f;
            foreach (var orderItem in orderItems)
            {
                totalRating += orderItem.Feedback.Rating;
            }
            float averageRating = totalRating / orderItems.Count;
            var result = await _context.BoxOptions.Where(bo => bo.BoxOptionId == boxOptionId).ExecuteUpdateAsync(setter => setter.SetProperty(bo => bo.Rating, averageRating));
            await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
