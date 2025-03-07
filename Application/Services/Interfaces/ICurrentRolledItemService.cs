using Common.Model.CurrentRolledITemDTOs.Request;

namespace Application.Services.Interfaces
{
    public interface ICurrentRolledItemService
    {
        Task<IEnumerable<CurrentRolledItemDto>> GetAllCurrentRolledItemByOnlineSerieBoxId(int onlineSerieBoxId);
        Task<bool> AddCurrentRolledItem(CurrentRolledItemDto currentRolledItemDtos);
        Task ResetCurrentRoll(int onlineSerieBoxId);
    }
}
