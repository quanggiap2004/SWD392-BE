using Common.Model.OnlineSerieBoxDTOs.Response;
using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IOnlineSerieBoxRepository
    {
        Task<OnlineSerieBox> AddOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox);
        Task<OnlineSerieBox?> GetOnlineSerieBoxByIdAsync(int id);
        Task<OnlineSerieBox> UpdateOnlineSerieBoxAsync(OnlineSerieBox onlineSerieBox);
        Task<IEnumerable<GetAllOnlineSerieBoxResponse>> GetAllOnlineSerieBoxesAsync();
        Task<OnlineSerieBoxDetailResponse> GetOnlineSerieBoxDetail(int onlineSerieBoxId);
        Task<bool> UpdatePublishStatusAsync(bool status, int id);
    }
}
