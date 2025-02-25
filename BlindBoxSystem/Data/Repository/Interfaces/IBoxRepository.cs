﻿using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxDTOs.ResponseDTOs;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IBoxRepository
    {
        Task<IEnumerable<Box>> GetAllBoxesAsync();

        Task<Box> GetBoxByIdAsync(int id);
        Task<Box> GetBoxByIdDTO(int id);
        Task<Box> AddBoxAsync(Box Box);
        Task<Box> UpdateBoxAsync(Box Box);
        Task DeleteBoxAsync(int id);
        Task<IEnumerable<AllBoxesDto>> GetAllBox();
        Task<AllBoxesDto?> GetBoxByIdV2(int id);
        Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBox(int quantity);
        Task<IEnumerable<AllBoxesDto>> GetBoxByBrand(int brandId);
    }
}
