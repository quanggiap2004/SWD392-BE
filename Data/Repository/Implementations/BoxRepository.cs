using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Domain.Domain.Model.BoxDTOs.ResponseDTOs;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
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

        public async Task<IEnumerable<AllBoxesDto>> GetAllBox()
        {
            return await _context.Boxes
        .AsNoTracking()
        .Where(b => !b.IsDeleted)
        .Select(b => new AllBoxesDto
        {
            boxId = b.BoxId,
            boxName = b.BoxName,
            boxDescription = b.BoxDescription,
            isDeleted = b.IsDeleted,
            soldQuantity = b.SoldQuantity,
            brandId = b.BrandId,
            brandName = b.Brand.BrandName,
            imageUrl = b.BoxImages.Select(img => img.BoxImageUrl),
            boxOptionIds = b.BoxOptions.Select(opt => opt.BoxOptionId)
        })
        .ToListAsync();
        }

        public async Task<AllBoxesDto?> GetBoxByIdV2(int id)
        {
            return await _context.Boxes.AsNoTracking()
                .Where(b => b.BoxId == id)
                .Select(b => new AllBoxesDto
                {
                    boxId = b.BoxId,
                    boxName = b.BoxName,
                    boxDescription = b.BoxDescription,
                    isDeleted = b.IsDeleted,
                    soldQuantity = b.SoldQuantity,
                    brandId = b.BrandId,
                    brandName = b.Brand.BrandName,
                    imageUrl = b.BoxImages.Select(img => img.BoxImageUrl),
                    boxOptionIds = b.BoxOptions.Select(opt => opt.BoxOptionId)
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBox(int quantity)
        {
            return await _context.Boxes.AsNoTracking()
                .Where(b => !b.IsDeleted)
                .OrderByDescending(b => b.SoldQuantity)
                .Select(b => new BestSellerBoxesDto
                {
                    boxId = b.BoxId,
                    boxName = b.BoxName,
                    boxDescription = b.BoxDescription,
                    soldQuantity = b.SoldQuantity,
                    brandId = b.BrandId,
                    brandName = b.Brand.BrandName,
                    imageUrl = b.BoxImages.Select(img => img.BoxImageUrl),
                    boxOptionIds = b.BoxOptions.Select(opt => opt.BoxOptionId)
                })
                .Take(quantity)
                .ToListAsync();
        }

        public async Task<IEnumerable<AllBoxesDto>> GetBoxByBrand(int brandId)
        {
            return await _context.Boxes.AsNoTracking()
                .Where(b => b.BrandId == brandId && b.IsDeleted == false)
                .Select(b => new AllBoxesDto
                {
                    boxId = b.BoxId,
                    boxName = b.BoxName,
                    boxDescription = b.BoxDescription,
                    isDeleted = b.IsDeleted,
                    soldQuantity = b.SoldQuantity,
                    brandId = b.BrandId,
                    brandName = b.Brand.BrandName,
                    imageUrl = b.BoxImages.Select(img => img.BoxImageUrl),
                    boxOptionIds = b.BoxOptions.Select(opt => opt.BoxOptionId)
                }).ToListAsync();
        }
        public async Task<IEnumerable<Box>> SearchBoxesByNameAsync(string? boxName)
        {
            if (string.IsNullOrEmpty(boxName))
            {
                return await _context.Boxes
                 .Include(b => b.Brand)
                .Include(bImage => bImage.BoxImages)
                .Include(bItem => bItem.BoxItems)
                .Include(bOnline => bOnline.OnlineSerieBoxes)
                .Include(bOption => bOption.BoxOptions)
                .Where(b => !b.IsDeleted).ToListAsync();
            }

            return await _context.Boxes
                .Include(b => b.Brand)
                .Include(bImage => bImage.BoxImages)
                .Include(bItem => bItem.BoxItems)
                .Include(bOnline => bOnline.OnlineSerieBoxes)
                .Include(bOption => bOption.BoxOptions)
                .Where(b => b.BoxName.Contains(boxName) && !b.IsDeleted)
                .ToListAsync();
        }
    }
}
