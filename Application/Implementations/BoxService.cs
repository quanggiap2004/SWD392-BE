using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxDTOs;
using BlindBoxSystem.Domain.Model.BoxImageDTOs;
using BlindBoxSystem.Domain.Model.BoxItemDTOs;
using BlindBoxSystem.Domain.Model.BoxOptionDTOs;
using BlindBoxSystem.Domain.Model.OnlineSerieBoxDTOs;

namespace BlindBoxSystem.Application.Implementations
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
                    BoxOptionPrice = boption.BoxOptionPrice,
                    BoxOptionStock = boption.BoxOptionStock,
                    OriginPrice = boption.OriginPrice,
                    DisplayPrice = boption.DisplayPrice,
                    IsDeleted = boption.IsDeleted,
                }).ToList() ?? new List<BoxOptionDTO>(),

                OnlineSerieBox = b.OnlineSerieBoxes?.Select(bOnline => new OnlineSerieBoxDTO
                {
                    BoxId = bOnline.BoxId,
                    OnlineSerieBoxId = bOnline.OnlineSerieBoxId,
                    IsSecretOpen = bOnline.IsSecretOpen,
                    Price = bOnline.Price,
                    Name = bOnline.Name,
                    Turn = bOnline.Turn,
                }).ToList() ?? new List<OnlineSerieBoxDTO>(),
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
                    BoxOptionPrice = boption.BoxOptionPrice,
                    BoxOptionStock = boption.BoxOptionStock,
                    OriginPrice = boption.OriginPrice,
                    DisplayPrice = boption.DisplayPrice,
                    IsDeleted = boption.IsDeleted,
                }).ToList() ?? new List<BoxOptionDTO>(),

                OnlineSerieBox = box.OnlineSerieBoxes?.Select(bOnline => new OnlineSerieBoxDTO
                {
                    BoxId = bOnline.BoxId,
                    OnlineSerieBoxId = bOnline.BoxId,
                    IsSecretOpen = bOnline.IsSecretOpen,
                    Price = bOnline.Price,
                    Name = bOnline.Name,
                    Turn = bOnline.Turn,
                }).ToList() ?? new List<OnlineSerieBoxDTO>(),
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
    }
}
