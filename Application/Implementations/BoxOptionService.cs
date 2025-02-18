using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxDTOs;
using BlindBoxSystem.Domain.Model.BoxOptionDTOs;

namespace BlindBoxSystem.Application.Implementations
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
                BoxOptionPrice = bOption.BoxOptionPrice,
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
                BoxOptionPrice = boxOption.BoxOptionPrice,
                BoxOptionStock = boxOption.BoxOptionStock,
                OriginPrice = boxOption.OriginPrice,
                DisplayPrice = boxOption.DisplayPrice,
                IsDeleted = boxOption.IsDeleted,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = boxOption.BoxId,
                    BoxName = boxOption.Box.BoxName,
                }
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
            existingBoxOption.BoxOptionPrice = boxOption.BoxOptionPrice;
            existingBoxOption.BoxOptionStock = boxOption.BoxOptionStock;
            existingBoxOption.OriginPrice = boxOption.OriginPrice;
            existingBoxOption.DisplayPrice = boxOption.DisplayPrice;
            existingBoxOption.IsDeleted = boxOption.IsDeleted;
            existingBoxOption.BoxId = boxOption.BoxId;

            return await _boxOptionRepository.UpdateBoxOptionAsync(existingBoxOption);
        }
    }
}
