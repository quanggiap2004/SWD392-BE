using Application.Services.Interfaces;
using Common.Model.BoxDTOs;
using Common.Model.BoxOptionDTOs;
using Common.Model.OrderItem;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;

namespace Application.Services.Implementations
{
    public class BoxOptionService : IBoxOptionService
    {

        private readonly IBoxOptionRepository _boxOptionRepository;
        public BoxOptionService(IBoxOptionRepository boxOptionRepository)
        {
            _boxOptionRepository = boxOptionRepository;
        }

        public async Task<BoxOption> AddBoxOptionAsync(BoxOption boxOption)
        {
            var addedBoxOption = await _boxOptionRepository.AddBoxOptionAsync(boxOption);
            return addedBoxOption;
        }

        public async Task DeleteBoxOptionAsync(int id)
        {
            var existingBoxOption = await _boxOptionRepository.GetBoxOptionByIdAsync(id);

            if (existingBoxOption != null)
            {
                await _boxOptionRepository.DeleteBoxOptionAsync(id);
            }
        }

        public async Task<IEnumerable<GetAllBoxOptionDTO>> GetAllBoxOptions()
        {
            var boxOptions = await _boxOptionRepository.GetAllBoxOptionsAsync();
            var boxOptionsDTO = boxOptions.Select(bOption => new GetAllBoxOptionDTO
            {
                BoxOptionId = bOption.BoxOptionId,
                BoxOptionName = bOption.BoxOptionName,
                BoxOptionStock = bOption.BoxOptionStock,
                OriginPrice = bOption.OriginPrice,
                DisplayPrice = bOption.DisplayPrice,
                IsDeleted = bOption.IsDeleted,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = bOption.BoxId,
                    BoxName = bOption.Box.BoxName,
                }

            });
            return boxOptionsDTO;
        }

        public async Task<BoxOption> GetBoxOptionById(int id)
        {
            var boxOption = await _boxOptionRepository.GetBoxOptionByIdAsync(id);
            return boxOption;
        }

        public async Task<GetAllBoxOptionDTO> GetBoxOptionDTO(int id)
        {
            var boxOption = await _boxOptionRepository.GetBoxOptionByIdDTO(id);
            if (boxOption == null)
            {
                return null;
            }
            var boxOptionDTO = new GetAllBoxOptionDTO
            {
                BoxOptionId = boxOption.BoxOptionId,
                BoxOptionName = boxOption.BoxOptionName,
                BoxOptionStock = boxOption.BoxOptionStock,
                OriginPrice = boxOption.OriginPrice,
                DisplayPrice = boxOption.DisplayPrice,
                IsDeleted = boxOption.IsDeleted,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = boxOption.BoxId,
                    BoxName = boxOption.Box.BoxName,
                },
                isOnlineSerieBox = boxOption.IsOnlineSerieBox
            };
            return boxOptionDTO;
        }

        public async Task<BoxOption> UpdateBoxOptionAsync(int id, BoxOption boxOption)
        {
            var existingBoxOption = await _boxOptionRepository.GetBoxOptionByIdAsync(id);
            if (existingBoxOption == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBoxOption.BoxOptionName = boxOption.BoxOptionName;
            existingBoxOption.BoxOptionStock = boxOption.BoxOptionStock;
            existingBoxOption.OriginPrice = boxOption.OriginPrice;
            existingBoxOption.DisplayPrice = boxOption.DisplayPrice;
            existingBoxOption.IsDeleted = boxOption.IsDeleted;
            existingBoxOption.BoxId = boxOption.BoxId;

            return await _boxOptionRepository.UpdateBoxOptionAsync(existingBoxOption);
        }

        public async Task<bool> UpdateStockQuantity(ICollection<OrderItemSimpleDto> orderItems)
        {
            return await _boxOptionRepository.UpdateStockQuantity(orderItems);
        }

        public async Task ReduceStockQuantity(ICollection<OrderItem> orderItems)
        {
            await _boxOptionRepository.ReduceStockQuantity(orderItems);
        }

        public async Task<bool> UpdateAverageBoxOptionRating(int boxOptionId)
        {
            return await _boxOptionRepository.UpdateAverageBoxOptionRating(boxOptionId);
        }
    }
}
