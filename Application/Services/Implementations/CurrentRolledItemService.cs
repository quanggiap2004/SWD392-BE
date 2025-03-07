using Application.Services.Interfaces;
using AutoMapper;
using Common.Model.CurrentRolledITemDTOs.Request;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;

namespace Application.Services.Implementations
{
    public class CurrentRolledItemService : ICurrentRolledItemService
    {
        private readonly ICurrentRolledItemRepository _currentRolledItemRepository;
        private readonly IMapper _mapper;
        public CurrentRolledItemService(ICurrentRolledItemRepository currentRolledItemRepository, IMapper mapper)
        {
            _currentRolledItemRepository = currentRolledItemRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddCurrentRolledItem(CurrentRolledItemDto currentRolledItemDtos)
        {
            var currentRolledItem = _mapper.Map<CurrentRolledItem>(currentRolledItemDtos);
            bool result = await _currentRolledItemRepository.AddCurrentRolledItem(currentRolledItem);
            return true;
        }

        public async Task<IEnumerable<CurrentRolledItemDto>> GetAllCurrentRolledItemByOnlineSerieBoxId(int onlineSerieBoxId)
        {
            return await _currentRolledItemRepository.GetCurrentRolledItemsByOnlineSerieBoxId(onlineSerieBoxId);
        }

        public async Task ResetCurrentRoll(int onlineSerieBoxId)
        {
            await _currentRolledItemRepository.ResetCurrentRoll(onlineSerieBoxId);
        }
    }
}
