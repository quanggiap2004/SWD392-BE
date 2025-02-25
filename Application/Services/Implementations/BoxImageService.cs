using Application.Services.Interfaces;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Domain.Domain.Model.BoxDTOs;
using Domain.Domain.Model.BoxImageDTOs;

namespace Application.Services.Implementations
{
    public class BoxImageService : IBoxImageService
    {

        private readonly IBoxImageRepository _boxImageRepository;
        public BoxImageService(IBoxImageRepository boxImageRepository)
        {
            _boxImageRepository = boxImageRepository;
        }

        public async Task<BoxImage> AddBoxImageAsync(BoxImage boxImage)
        {
            var addedBoxImage = await _boxImageRepository.AddBoxImageAsync(boxImage);
            return addedBoxImage;
        }

        public async Task DeleteBoxImageAsync(int id)
        {
            var existingBoxImage = await _boxImageRepository.GetBoxImageByIdAsync(id);

            if (existingBoxImage != null)
            {
                await _boxImageRepository.DeleteBoxImageAsync(id);
            }
        }

        public async Task<IEnumerable<GetAllBoxImageDTO>> GetAllBoxesImage()
        {
            var boxImages = await _boxImageRepository.GetAllBoxImageAsync();
            var boxImagesDTO = boxImages.Select(bImage => new GetAllBoxImageDTO
            {
                BoxImageId = bImage.BoxImageId,
                BoxImageUrl = bImage.BoxImageUrl,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = bImage.BoxId,
                    BoxName = bImage.Box.BoxName,
                }
            });
            return boxImagesDTO;
        }

        public async Task<BoxImage> GetBoxImageById(int id)
        {
            var boxImage = await _boxImageRepository.GetBoxImageByIdAsync(id);
            return boxImage;
        }

        public async Task<GetAllBoxImageDTO> GetBoxImageDTO(int id)
        {
            var boxImage = await _boxImageRepository.GetBoxImageByIdDTO(id);
            if (boxImage == null)
            {
                return null;
            }
            var boxImageDTO = new GetAllBoxImageDTO
            {
                BoxImageId = boxImage.BoxImageId,
                BoxImageUrl = boxImage.BoxImageUrl,
                BelongBox = new BelongBoxResponseDTO
                {
                    BoxId = boxImage.BoxId,
                    BoxName = boxImage.Box.BoxName,
                }
            };
            return boxImageDTO;

        }

        public async Task<BoxImage> UpdateBoxImageAsync(int id, BoxImage boxImage)
        {
            var existingBoxImage = await _boxImageRepository.GetBoxImageByIdAsync(id);
            if (existingBoxImage == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBoxImage.BoxImageUrl = boxImage.BoxImageUrl;
            existingBoxImage.BoxId = boxImage.BoxId;

            return await _boxImageRepository.UpdateBoxImageAsync(existingBoxImage);
        }
    }
}
