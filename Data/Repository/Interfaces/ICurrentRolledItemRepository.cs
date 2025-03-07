using Common.Model.CurrentRolledITemDTOs.Request;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface ICurrentRolledItemRepository
    {
        Task<bool> AddCurrentRolledItem(CurrentRolledItem currentRolledItem);
        Task<IEnumerable<CurrentRolledItemDto>> GetCurrentRolledItemsByOnlineSerieBoxId(int onlineSerieBoxId);
        Task ResetCurrentRoll(int onlineSerieBoxId);
    }
}
