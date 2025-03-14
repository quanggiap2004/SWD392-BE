using Common.Exceptions;
using Common.Model.BoxDTOs.ResponseDTOs;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.BrandDTOs.Response;
using Common.Model.OnlineSerieBoxDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class OnlineSerieBoxRepository : IOnlineSerieBoxRepository
    {
        private readonly BlindBoxSystemDbContext _context;

        public OnlineSerieBoxRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<OnlineSerieBox> AddOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox)
        {
            _context.OnlineSerieBoxes.Add(onlineSerieBox);
            await _context.SaveChangesAsync();
            return onlineSerieBox;
        }

        public async Task<IEnumerable<GetAllOnlineSerieBoxResponse>> GetAllOnlineSerieBoxesAsync()
        {
            return await _context.OnlineSerieBoxes
                .Select(o => new GetAllOnlineSerieBoxResponse
                {
                    onlineSerieBoxId = o.OnlineSerieBoxId,
                    priceAfterSecret = o.PriceAfterSecret,
                    priceIncreasePercent = o.PriceIncreasePercent,
                    maxTurn = o.MaxTurn,
                    turn = o.Turn,
                    isSecretOpen = o.IsSecretOpen,
                    imageUrl = o.ImageUrl,
                    basePrice = o.BasePrice,
                    isPublished = o.IsPublished,
                    boxOption = new BoxOptionResponse
                    {
                        boxOptionId = o.BoxOption.BoxOptionId,
                        boxOptionName = o.BoxOption.BoxOptionName,
                        originPrice = o.BoxOption.OriginPrice,
                        displayPrice = o.BoxOption.DisplayPrice,
                        boxOptionStock = o.BoxOption.BoxOptionStock,
                        simpleBoxDtoResponse = new SimpleBoxDtoResponse
                        {
                            boxId = o.BoxOption.Box.BoxId,
                            boxName = o.BoxOption.Box.BoxName,
                        }
                    },
                    brandDtoResponse = new BrandDtoResponse
                    {
                        brandId = o.BoxOption.Box.BrandId,
                        brandName = o.BoxOption.Box.Brand.BrandName,
                        imageUrl = o.BoxOption.Box.Brand.ImageUrl
                    }
                }).ToListAsync();
        }

        public async Task<OnlineSerieBox?> GetOnlineSerieBoxByIdAsync(int id)
        {
            return await _context.OnlineSerieBoxes.Include(o => o.BoxOption).FirstOrDefaultAsync(o => o.OnlineSerieBoxId == id);
        }

        public async Task<OnlineSerieBoxDetailResponse> GetOnlineSerieBoxDetail(int onlineSerieBoxId)
        {
            return await _context.OnlineSerieBoxes.Where(o => o.OnlineSerieBoxId == onlineSerieBoxId)
                .Select(o => new OnlineSerieBoxDetailResponse
                {
                    onlineSerieBoxId = o.OnlineSerieBoxId,
                    priceAfterSecret = o.PriceAfterSecret,
                    priceIncreasePercent = o.PriceIncreasePercent,
                    maxTurn = o.MaxTurn,
                    turn = o.Turn,
                    isSecretOpen = o.IsSecretOpen,
                    imageUrl = o.ImageUrl,
                    basePrice = o.BasePrice,
                    isPublished = o.IsPublished,
                    boxOption = new BoxOptionResponse
                    {
                        boxOptionId = o.BoxOption.BoxOptionId,
                        boxOptionName = o.BoxOption.BoxOptionName,
                        originPrice = o.BoxOption.OriginPrice,
                        displayPrice = o.BoxOption.DisplayPrice,
                        boxOptionStock = o.BoxOption.BoxOptionStock,
                        simpleBoxDtoResponse = new SimpleBoxDtoResponse
                        {
                            boxId = o.BoxOption.Box.BoxId,
                            boxName = o.BoxOption.Box.BoxName,
                        }
                    },
                    brandDtoResponse = new BrandDtoResponse
                    {
                        brandId = o.BoxOption.Box.BrandId,
                        brandName = o.BoxOption.Box.Brand.BrandName,
                        imageUrl = o.BoxOption.Box.Brand.ImageUrl
                    },
                    boxItemResponseDtos = o.BoxOption.Box.BoxItems.Select(b => new BoxItemResponseDto
                    {
                        boxItemId = b.BoxItemId,
                        boxItemName = b.BoxItemName,
                        boxItemDescription = b.BoxItemDescription,
                        boxItemEyes = b.BoxItemEyes,
                        boxItemColor = b.BoxItemColor,
                        averageRating = b.AverageRating,
                        boxId = b.BoxId,
                        imageUrl = b.ImageUrl,
                        numOfVote = b.NumOfVote,
                        isSecret = b.IsSecret
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task<OnlineSerieBox> UpdateOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox)
        {
            var onlineSerieBoxToUpdate = await _context.OnlineSerieBoxes.FindAsync(onlineSerieBox.OnlineSerieBoxId);
            if (onlineSerieBoxToUpdate == null)
            {
                throw new CustomExceptions.NotFoundException($"OnlineSerieBox with ID {onlineSerieBox.OnlineSerieBoxId} not found.");
            }
            onlineSerieBoxToUpdate.IsSecretOpen = onlineSerieBox.IsSecretOpen;
            onlineSerieBoxToUpdate.MaxTurn = onlineSerieBox.MaxTurn;
            onlineSerieBoxToUpdate.PriceAfterSecret = onlineSerieBox.PriceAfterSecret;
            onlineSerieBoxToUpdate.PriceIncreasePercent = onlineSerieBox.PriceIncreasePercent;
            onlineSerieBoxToUpdate.ImageUrl = onlineSerieBox.ImageUrl;
            await _context.SaveChangesAsync();
            return onlineSerieBox;
        }

        public async Task<bool> UpdatePublishStatusAsync(bool status, int id)
        {
            return await _context.OnlineSerieBoxes.Where(o => o.OnlineSerieBoxId == id).ExecuteUpdateAsync(setter => setter.SetProperty(o => o.IsPublished, status)) > 0;
        }
    }
}
