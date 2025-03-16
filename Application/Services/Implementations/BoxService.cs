using Application.Services.Interfaces;
using Common.Model.BoxDTOs;
using Common.Model.BoxDTOs.ResponseDTOs;
using Common.Model.BoxImageDTOs;
using Common.Model.BoxItemDTOs;
using Common.Model.BoxOptionDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using static Common.Exceptions.CustomExceptions;

namespace Application.Services.Implementations
{
    public class BoxService : IBoxService
    {
        private readonly IBoxRepository _boxRepository;

        public BoxService(IBoxRepository boxRepository)
        {
            _boxRepository = boxRepository;
        }

        public async Task<Box> AddBoxAsync(Box box)
        {
            var addedBox = await _boxRepository.AddBoxAsync(box);
            return addedBox;
        }

        public async Task DeleteBoxAsync(int id)
        {
            var existingBox = await _boxRepository.GetBoxByIdAsync(id);

            if (existingBox != null)
            {
                await _boxRepository.DeleteBoxAsync(id);
            }
        }

        public async Task<IEnumerable<AllBoxesDto>> GetAllBox()
        {
            return await _boxRepository.GetAllBox();
        }

        public async Task<IEnumerable<GetAllBoxesDTO>> GetAllBoxes()
        {
            var boxes = await _boxRepository.GetAllBoxesAsync();
            var boxesDTO = boxes.Select(b => new GetAllBoxesDTO
            {
                BoxId = b.BoxId,
                BoxName = b.BoxName,
                BoxDescription = b.BoxDescription,
                IsDeleted = b.IsDeleted,
                SoldQuantity = b.SoldQuantity,
                BrandId = b.BrandId,
                BrandName = b.Brand?.BrandName,

                BoxImage = b.BoxImages?.Select(bimage => new BoxImageDTO
                {
                    BoxId = bimage.BoxId,
                    BoxImageId = bimage.BoxImageId,
                    BoxImageUrl = bimage.BoxImageUrl,
                }).ToList() ?? new List<BoxImageDTO>(),

                BoxItem = b.BoxItems?.Select(bitem => new BoxItemDTO
                {
                    BoxId = bitem.BoxId,
                    BoxItemId = bitem.BoxItemId,
                    BoxItemName = bitem.BoxItemName,
                    BoxItemColor = bitem.BoxItemColor,
                    BoxItemDescription = bitem.BoxItemDescription,
                    BoxItemEyes = bitem.BoxItemEyes,
                    AverageRating = bitem.AverageRating,
                    ImageUrl = bitem.ImageUrl,
                    NumOfVote = bitem.NumOfVote,
                    IsSecret = bitem.IsSecret,
                }).ToList() ?? new List<BoxItemDTO>(),

                BoxOptions = b.BoxOptions?.Select(boption => new BoxOptionDTO
                {
                    BoxId = boption.BoxId,
                    BoxOptionId = boption.BoxOptionId,
                    BoxOptionName = boption.BoxOptionName,
                    BoxOptionStock = boption.BoxOptionStock,
                    OriginPrice = boption.OriginPrice,
                    DisplayPrice = boption.DisplayPrice,
                    IsDeleted = boption.IsDeleted,
                    IsOnlineSerieBox = boption.IsOnlineSerieBox,
                }).ToList() ?? new List<BoxOptionDTO>(),

                //OnlineSerieBox = b.OnlineSerieBoxes?.Select(bOnline => new OnlineSerieBoxDTO
                //{
                //    BoxId = bOnline.Box,
                //    OnlineSerieBoxId = bOnline.OnlineSerieBoxId,
                //    IsSecretOpen = bOnline.IsSecretOpen,
                //    Price = bOnline.Price,
                //    Name = bOnline.Name,
                //    Turn = bOnline.Turn,
                //}).ToList() ?? new List<OnlineSerieBoxDTO>(),
            });
            return boxesDTO;
        }

        public async Task<Box> GetBoxById(int id)
        {
            var box = await _boxRepository.GetBoxByIdAsync(id);
            return box;
        }

        public async Task<GetAllBoxesDTO> GetBoxByIdDTO(int id)
        {
            var box = await _boxRepository.GetBoxByIdDTO(id);
            if (box == null)
            {
                return null;
            }

            var boxDTO = new GetAllBoxesDTO
            {
                BoxId = box.BoxId,
                BoxName = box.BoxName,
                BoxDescription = box.BoxDescription,
                IsDeleted = box.IsDeleted,
                SoldQuantity = box.SoldQuantity,
                BrandId = box.BrandId,
                BrandName = box.Brand?.BrandName,

                BoxImage = box.BoxImages?.Select(bimage => new BoxImageDTO
                {
                    BoxId = bimage.BoxId,
                    BoxImageId = bimage.BoxImageId,
                    BoxImageUrl = bimage.BoxImageUrl,
                }).ToList() ?? new List<BoxImageDTO>(),

                BoxItem = box.BoxItems?.Select(bitem => new BoxItemDTO
                {
                    BoxId = bitem.BoxId,
                    BoxItemId = bitem.BoxItemId,
                    BoxItemName = bitem.BoxItemName,
                    BoxItemColor = bitem.BoxItemColor,
                    BoxItemDescription = bitem.BoxItemDescription,
                    BoxItemEyes = bitem.BoxItemEyes,
                    AverageRating = bitem.AverageRating,
                    ImageUrl = bitem.ImageUrl,
                    NumOfVote = bitem.NumOfVote,
                    IsSecret = bitem.IsSecret,
                }).ToList() ?? new List<BoxItemDTO>(),

                BoxOptions = box.BoxOptions?.Select(boption => new BoxOptionDTO
                {
                    BoxId = boption.BoxId,
                    BoxOptionId = boption.BoxOptionId,
                    BoxOptionName = boption.BoxOptionName,
                    BoxOptionStock = boption.BoxOptionStock,
                    OriginPrice = boption.OriginPrice,
                    DisplayPrice = boption.DisplayPrice,
                    IsDeleted = boption.IsDeleted,
                    IsOnlineSerieBox = boption.IsOnlineSerieBox,

                }).ToList() ?? new List<BoxOptionDTO>(),

                //OnlineSerieBox = box.OnlineSerieBoxes?.Select(bOnline => new OnlineSerieBoxDTO
                //{
                //    BoxId = bOnline.BoxId,
                //    OnlineSerieBoxId = bOnline.BoxId,
                //    IsSecretOpen = bOnline.IsSecretOpen,
                //    Price = bOnline.Price,
                //    Name = bOnline.Name,
                //    Turn = bOnline.Turn,
                //}).ToList() ?? new List<OnlineSerieBoxDTO>(),
            };
            return boxDTO;
        }


        public async Task<Box> UpdateBoxAsync(int id, Box box)
        {
            var existingBox = await _boxRepository.GetBoxByIdAsync(id);
            if (existingBox == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBox.BoxName = box.BoxName;
            existingBox.BoxDescription = box.BoxDescription;
            existingBox.BrandId = box.BrandId;

            return await _boxRepository.UpdateBoxAsync(existingBox);
        }


        public async Task<AllBoxesDto?> GetBoxByIdV2(int id)
        {
            var box = await _boxRepository.GetBoxByIdV2(id);
            if (box is null)
            {
                throw new NotFoundException($"Cannot found box with id:{id}");
            }
            return box;
        }

        public Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBox(int quantity)
        {
            return _boxRepository.GetBestSellerBox(quantity);
        }

        public async Task<IEnumerable<AllBoxesDto>> GetBoxByBrand(int brandId)
        {
            IEnumerable<AllBoxesDto> result = await _boxRepository.GetBoxByBrand(brandId);
            if (result == null)
            {
                throw new NotFoundException($"Cannot found box with brandId:{brandId}");
            }
            return result;
        }

        public async Task<IEnumerable<GetAllBoxesDTO>> SearchBoxesByNameAsync(string? boxName)
        {
            var searchBoxes = await _boxRepository.SearchBoxesByNameAsync(boxName);
            var boxesDTO = searchBoxes.Select(b => new GetAllBoxesDTO
            {
                BoxId = b.BoxId,
                BoxName = b.BoxName,
                BoxDescription = b.BoxDescription,
                IsDeleted = b.IsDeleted,
                SoldQuantity = b.SoldQuantity,
                BrandId = b.BrandId,
                BrandName = b.Brand?.BrandName,

                BoxImage = b.BoxImages?.Select(bimage => new BoxImageDTO
                {
                    BoxId = bimage.BoxId,
                    BoxImageId = bimage.BoxImageId,
                    BoxImageUrl = bimage.BoxImageUrl,
                }).ToList() ?? new List<BoxImageDTO>(),

                BoxItem = b.BoxItems?.Select(bitem => new BoxItemDTO
                {
                    BoxId = bitem.BoxId,
                    BoxItemId = bitem.BoxItemId,
                    BoxItemName = bitem.BoxItemName,
                    BoxItemColor = bitem.BoxItemColor,
                    BoxItemDescription = bitem.BoxItemDescription,
                    BoxItemEyes = bitem.BoxItemEyes,
                    AverageRating = bitem.AverageRating,
                    ImageUrl = bitem.ImageUrl,
                    NumOfVote = bitem.NumOfVote,
                    IsSecret = bitem.IsSecret,
                }).ToList() ?? new List<BoxItemDTO>(),

                BoxOptions = b.BoxOptions?.Select(boption => new BoxOptionDTO
                {
                    BoxId = boption.BoxId,
                    BoxOptionId = boption.BoxOptionId,
                    BoxOptionName = boption.BoxOptionName,
                    BoxOptionStock = boption.BoxOptionStock,
                    OriginPrice = boption.OriginPrice,
                    DisplayPrice = boption.DisplayPrice,
                    IsDeleted = boption.IsDeleted,
                }).ToList() ?? new List<BoxOptionDTO>(),

                //OnlineSerieBox = b.OnlineSerieBoxes?.Select(bOnline => new OnlineSerieBoxDTO
                //{
                //    BoxId = bOnline.BoxId,
                //    OnlineSerieBoxId = bOnline.OnlineSerieBoxId,
                //    IsSecretOpen = bOnline.IsSecretOpen,
                //    Price = bOnline.Price,
                //    Name = bOnline.Name,
                //    Turn = bOnline.Turn,
                //}).ToList() ?? new List<OnlineSerieBoxDTO>(),
            });
            return boxesDTO;
        }

        public async Task UpdateSoldQuantity(ICollection<OrderItem> orderItems)
        {
            await _boxRepository.UpdateSoldQuantity(orderItems);
        }

        public Task<BoxAndBoxItemResponseDto> getBoxByBoxOptionId(int boxOptionId)
        {
            var result = _boxRepository.getBoxByBoxOptionId(boxOptionId);
            return result;
        }

        public async Task<bool> UpdateBoxRatingByBoxOptionId(int boxOptionId)
        {
            return await _boxRepository.UpdateBoxRatingByBoxOptionId(boxOptionId);
        }
    }
}
