using Application.Services.Interfaces;
using Common.Model.BoxDTOs;
using Common.Model.BoxItemDTOs;
using Common.Model.UserVotedBoxItemDTOs;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;

namespace Application.Services.Implementations
{
    public class BoxItemService : IBoxItemService
    {

        private readonly IBoxItemRepository _boxItemRepository;
        private readonly IUserVotedBoxItemRepository _userVotedBoxItemRepository;

        public BoxItemService(IBoxItemRepository boxItemRepository, IUserVotedBoxItemRepository userVotedBoxItemRepository)
        {
            _boxItemRepository = boxItemRepository;
            _userVotedBoxItemRepository = userVotedBoxItemRepository;

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
                VotedResponse = boxItem.UserVotedBoxItems.Select(vote => new VotedResponseDTO
                {
                    boxItemId = vote.BoxItemId,
                    userId = vote.UserId,
                    rating = vote.Rating,
                }).ToList(),
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

        public async Task<GetAllVotedDTO> AddOrUpdateVoteAsync(AddVoteDTO addVoteDTO)
        {
            var result = await _userVotedBoxItemRepository.AddOrUpdateVoteAsync(addVoteDTO);
            var VotedDTO = new GetAllVotedDTO
            {
                UserVotedBoxItemId = result.UserVotedBoxItemId,
                BoxItemId = result.BoxItemId,
                UserId = result.UserId,
                Rating = result.Rating,
                LastUpdated = result.LastUpdated,
            };
            return VotedDTO;
        }
        public async Task<IEnumerable<GetAllVotedDTO>> GetVotesByBoxItemId(int id)
        {
            var Vote = await _userVotedBoxItemRepository.GetVotesByBoxItemIdAsync(id);
            var VotedDTO = Vote.Select(vote => new GetAllVotedDTO
            {
                UserVotedBoxItemId = vote.UserVotedBoxItemId,
                BoxItemId = vote.BoxItemId,
                UserId = vote.UserId,
                Username = vote.User.Username,
                Rating = vote.Rating,
                LastUpdated = vote.LastUpdated,
            }).ToList();
            return VotedDTO;
        }

        public async Task<ICollection<BoxItem>> GetBoxItemByBoxId(int id)
        {
            return await _boxItemRepository.GetBoxItemByBoxId(id);
        }
    }
}
