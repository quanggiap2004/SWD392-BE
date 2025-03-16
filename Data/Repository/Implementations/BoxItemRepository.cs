using AutoMapper;
using Common.Exceptions;
using Common.Model.BoxItemDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class BoxItemRepository : IBoxItemRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        private readonly IOnlineSerieBoxRepository _onlineSerieBoxRepository;
        private readonly IMapper _mapper;
        public BoxItemRepository(BlindBoxSystemDbContext context, IMapper mapper, IOnlineSerieBoxRepository onlineSerieBoxRepository)
        {
            _context = context;
            _mapper = mapper;
            _onlineSerieBoxRepository = onlineSerieBoxRepository;
        }

        public async Task<BoxItemResponseDto> AddBoxItemAsync(BoxItem boxItem)
        {
            var boxObject = await _context.Boxes.Include(b =>b.BoxItems).Include(b => b.BoxOptions).ThenInclude(bo => bo.OnlineSerieBox).Where(bo => bo.BoxId == boxItem.BoxId).FirstOrDefaultAsync();
            if (boxObject == null)
            {
                throw new CustomExceptions.NotFoundException("BoxId not found");
            }
            if (boxItem.IsSecret == true && boxObject.BoxItems.Any(bi => bi.IsSecret == true))
            {
                throw new CustomExceptions.BadRequestException("BoxOption already have an secret");
            }
            _context.BoxItems.Add(boxItem);
            await _context.SaveChangesAsync();
            foreach (var boxOption in boxObject.BoxOptions)
            {
                if (boxOption.IsOnlineSerieBox)
                {
                    boxOption.OnlineSerieBox.MaxTurn = boxObject.BoxItems.Count;
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<BoxItemResponseDto>(boxItem);
        }

        public async Task DeleteBoxItemAsync(int id)
        {
            var deletedBoxItem = await _context.BoxItems.Include(bi => bi.Box).ThenInclude(bi => bi.BoxOptions).ThenInclude(bo => bo.OnlineSerieBox).FirstOrDefaultAsync(bi => bi.BoxItemId == id);
            if (deletedBoxItem != null)
            {
                
                foreach (var boxOption in deletedBoxItem.Box.BoxOptions)
                {
                    if(boxOption.IsOnlineSerieBox)
                    {
                        boxOption.OnlineSerieBox.MaxTurn--;
                    }
                }
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
            return await _context.BoxItems.Include(b => b.Box).Include(vote => vote.UserVotedBoxItems).FirstOrDefaultAsync(b => b.BoxItemId == id);
        }

        public async Task<BoxItem> UpdateBoxItemAsync(BoxItem boxItem)
        {
            _context.BoxItems.Update(boxItem);
            await _context.SaveChangesAsync();
            return boxItem;
        }
        public async Task<ICollection<BoxItem>> GetBoxItemByBoxId(int id)
        {
            return await _context.BoxItems.Where(b => b.BoxId == id).ToListAsync();
        }
    }
}
