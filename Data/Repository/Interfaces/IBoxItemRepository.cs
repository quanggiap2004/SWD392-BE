﻿using Common.Model.BoxItemDTOs.Response;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IBoxItemRepository
    {
        Task<IEnumerable<BoxItem>> GetAllBoxItemAsync();

        Task<BoxItem> GetBoxItemByIdAsync(int id);
        Task<BoxItem> GetBoxItemByIdDTO(int id);
        Task<BoxItemResponseDto> AddBoxItemAsync(BoxItem boxItem);
        Task<BoxItem> UpdateBoxItemAsync(BoxItem boxItem);
        Task<bool> DeleteBoxItemAsync(int id);
        Task<ICollection<BoxItem>> GetBoxItemByBoxId(int id);
    }
}
