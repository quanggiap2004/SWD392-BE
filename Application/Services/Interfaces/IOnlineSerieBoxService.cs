using Common.Model.BoxItemDTOs.Response;
using Common.Model.OnlineSerieBoxDTOs.Request;
using Common.Model.OnlineSerieBoxDTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IOnlineSerieBoxService
    {
        Task<CreateBoxOptionAndOnlineSerieBoxResponse> CreateBoxOptionAndOnlineSerieBoxAsync(CreateBoxOptionAndOnlineSerieBoxRequest request);
        Task<UpdateOnlineSerieBoxResponse> UpdateOnlineSerieBoxAsync(int onlineSerieBoxId, UpdateOnlineSerieBoxRequest request);
        Task<IEnumerable<GetAllOnlineSerieBoxResponse>> GetAllOnlineSerieBoxesAsync();
        Task<BoxItemResponseDto> OpenOnlineSerieBoxAsync(OpenOnlineSerieBoxRequest request);
        Task<GetAllOnlineSerieBoxResponse> GetOnlineSerieBoxByIdAsync(int onlineSerieBoxId);
    }
}
