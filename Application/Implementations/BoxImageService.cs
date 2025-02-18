using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Data.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxImageDTOs;

namespace BlindBoxSystem.Application.Implementations
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

        public Task DeleteBoxImageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetAllBoxImageDTO>> GetAllBoxesImage()
        {
            throw new NotImplementedException();
        }

        public Task<BoxImage> GetBoxImageById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllBoxImageDTO> GetBoxImageDTO(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BoxImage> UpdateBoxImageAsync(int id, BoxImage boxImage)
        {
            throw new NotImplementedException();
        }
    }
}
