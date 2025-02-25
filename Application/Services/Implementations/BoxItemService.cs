using Application.Services.Interfaces;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Domain.Domain.Model.BoxDTOs;
using Domain.Domain.Model.BoxItemDTOs;

namespace Application.Services.Implementations
{
    public class BoxItemService : IBoxItemService
    {

        private readonly IBoxItemRepository _boxItemRepository;
        public BoxItemService(IBoxItemRepository boxItemRepository)
        {
            _boxItemRepository = boxItemRepository;
        }
        public async Task<BoxItem> AddBoxItemAsync(BoxItem boxItem)
        {
            var addedBoxitem = await _boxItemRepository.AddBoxItemAsync(boxItem);
            return addedBoxitem;
        }

        public async Task DeleteBoxItemAsync(int id)
        {
            var existingBoxItem = await _boxItemRepository.GetBoxItemByIdAsync(id);

            if (existingBoxItem != null)
            {
                await _boxItemRepository.DeleteBoxItemAsync(id);
            }
        }

        public async Task<IEnumerable<GetAllBoxItemDTO>> GetAllBoxItems()
        {
            var boxItems = await _boxItemRepository.GetAllBoxItemAsync();
            var boxItemsDTO = boxItems.Select(bItem => new GetAllBoxItemDTO
            {
                BoxItemId = bItem.BoxItemId,
                BoxItemName = bItem.BoxItemName,
                BoxItemDescription = bItem.BoxItemDescription,
                BoxItemColor = bItem.BoxItemColor,
                BoxItemEyes = bItem.BoxItemEyes,
                AverageRating = bItem.AverageRating,
                ImageUrl = bItem.ImageUrl,
                NumOfVote = bItem.NumOfVote,
                IsSecret = bItem.IsSecret,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = bItem.BoxId,
                    BoxName = bItem.Box.BoxName,
                }

            });
            return boxItemsDTO;
        }

        public async Task<BoxItem> GetBoxItemById(int id)
        {
            var boxItem = await _boxItemRepository.GetBoxItemByIdAsync(id);
            return boxItem;
        }

        public async Task<GetAllBoxItemDTO> GetBoxItemDTO(int id)
        {
            var boxItem = await _boxItemRepository.GetBoxItemByIdDTO(id);
            if (boxItem == null)
            {
                return null;
            }
            var boxItemDTO = new GetAllBoxItemDTO
            {
                BoxItemId = boxItem.BoxItemId,
                BoxItemName = boxItem.BoxItemName,
                BoxItemDescription = boxItem.BoxItemDescription,
                BoxItemColor = boxItem.BoxItemColor,
                BoxItemEyes = boxItem.BoxItemEyes,
                AverageRating = boxItem.AverageRating,
                ImageUrl = boxItem.ImageUrl,
                NumOfVote = boxItem.NumOfVote,
                IsSecret = boxItem.IsSecret,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = boxItem.BoxId,
                    BoxName = boxItem.Box.BoxName,
                }
            };
            return boxItemDTO;
        }

        public async Task<BoxItem> UpdateBoxItemAsync(int id, BoxItem boxItem)
        {
            var existingBoxItem = await _boxItemRepository.GetBoxItemByIdAsync(id);
            if (existingBoxItem == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBoxItem.BoxItemName = boxItem.BoxItemName;
            existingBoxItem.BoxItemDescription = boxItem.BoxItemDescription;
            existingBoxItem.BoxItemColor = boxItem.BoxItemColor;
            existingBoxItem.BoxItemEyes = boxItem.BoxItemEyes;
            existingBoxItem.ImageUrl = boxItem.ImageUrl;
            existingBoxItem.IsSecret = boxItem.IsSecret;
            existingBoxItem.BoxId = boxItem.BoxId;

            return await _boxItemRepository.UpdateBoxItemAsync(existingBoxItem);
        }
    }
}
