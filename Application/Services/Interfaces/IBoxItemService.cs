﻿using Common.Model.BoxItemDTOs;
using Common.Model.UserVotedBoxItemDTOs;
using Domain.Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IBoxItemService
    {
        Task<IEnumerable<GetAllBoxItemDTO>> GetAllBoxItems();
        Task<BoxItem> GetBoxItemById(int id);
        Task<GetAllBoxItemDTO> GetBoxItemDTO(int id);
        Task<BoxItem> AddBoxItemAsync(BoxItem boxItem);
        Task<BoxItem> UpdateBoxItemAsync(int id, BoxItem boxItem);
        Task DeleteBoxItemAsync(int id);
        Task<GetAllVotedDTO> AddOrUpdateVoteAsync(AddVoteDTO addVoteDTO);
        Task<IEnumerable<GetAllVotedDTO>> GetVotesByBoxItemId(int id);
        Task<ICollection<BoxItem>> GetBoxItemByBoxId(int id);
    }
}
