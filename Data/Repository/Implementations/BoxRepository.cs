using Common.Constants;
using Common.Model.BoxDTOs.ResponseDTOs;
using Common.Model.BoxItemDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
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
                deletedBox.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Box>> GetAllBoxesAsync()
        {
            return await _context.Boxes.Include(b => b.Brand)
                .Include(bImage => bImage.BoxImages)
                .Include(bItem => bItem.BoxItems)
                .Include(bOption => bOption.BoxOptions.Where(bo => bo.IsDeleted == false))
                .ToListAsync();
        }

        public async Task<Box> GetBoxByIdAsync(int id)
        {
            return await _context.Boxes.FindAsync(id);
        }

        public async Task<Box> GetBoxByIdDTO(int id)
        {
            return await _context.Boxes.Include(b => b.Brand)
                .Include(bImage => bImage.BoxImages)
                .Include(bItem => bItem.BoxItems)
                .Include(bOption => bOption.BoxOptions.Where(bo => bo.IsDeleted == false))
                .FirstOrDefaultAsync(box => box.BoxId == id);
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
                .Include(bOption => bOption.BoxOptions)
                .Where(b => !b.IsDeleted).ToListAsync();
            }

            return await _context.Boxes
                .Include(b => b.Brand)
                .Include(bImage => bImage.BoxImages)
                .Include(bItem => bItem.BoxItems)
                .Include(bOption => bOption.BoxOptions)
                .Where(b => b.BoxName.Contains(boxName) && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> UpdateSoldQuantity(ICollection<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
            {
                int soldQuantity = 0;
                var boxOption = await _context.BoxOptions.FindAsync(item.BoxOptionId);
                if (boxOption == null)
                {
                    return false;
                }
                var box = await _context.Boxes.FindAsync(boxOption.BoxId);
                var orderItemWithCondition = await _context.OrderItems.Where(oi =>
                                        oi.BoxOption.BoxId == box.BoxId
                                        &&
                                        oi.Order.CurrentOrderStatusId != (int)ProjectConstant.OrderStatus.Cancelled)
                                            .ToListAsync();
                foreach (var orderItem in orderItemWithCondition)
                {
                    soldQuantity += orderItem.Quantity;
                }
                box.SoldQuantity = soldQuantity;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BoxAndBoxItemResponseDto?> getBoxByBoxOptionId(int boxOptionId)
        {
            var boxOption = await _context.BoxOptions.FindAsync(boxOptionId);
            return await _context.Boxes.Where(b => b.BoxId == boxOption.BoxId).Select(box => new BoxAndBoxItemResponseDto
            {
                boxId = box.BoxId,
                boxName = box.BoxName,
                boxDescription = box.BoxDescription,
                isDeleted = box.IsDeleted,
                soldQuantity = box.SoldQuantity,
                brandId = box.BrandId,
                boxItems = box.BoxItems.Select(bItem => new BoxItemResponseDto
                {
                    boxItemId = bItem.BoxItemId,
                    boxItemName = bItem.BoxItemName,
                    boxItemDescription = bItem.BoxItemDescription,
                    boxItemColor = bItem.BoxItemColor,
                    boxItemEyes = bItem.BoxItemEyes,
                    averageRating = bItem.AverageRating,
                    imageUrl = bItem.ImageUrl,
                    isSecret = bItem.IsSecret,
                    numOfVote = bItem.NumOfVote
                }).ToList()
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBoxRatingByBoxOptionId(int boxOptionId)
        {
            var result = await _context.Boxes
                .Where(b => b.BoxOptions.Any(bo => bo.BoxOptionId == boxOptionId))
                .Select(b => new
                {
                    Box = b,
                    TotalRating = b.BoxOptions
                        .SelectMany(bo => bo.OrderItem)
                        .Where(oi => oi.Feedback != null)
                        .Sum(oi => (float?)oi.Feedback.Rating) ?? 0,
                    TotalFeedbacks = b.BoxOptions
                        .SelectMany(bo => bo.OrderItem)
                        .Count(oi => oi.Feedback != null)
                })
                .FirstOrDefaultAsync();

            if (result == null)
                return false;

            result.Box.Rating = result.TotalFeedbacks > 0
                                  ? result.TotalRating / result.TotalFeedbacks
                                  : 0;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
