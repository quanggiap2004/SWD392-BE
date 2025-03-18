using Common.Model.BoxDTOs.ResponseDTOs;
using Common.Model.DashboardDTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponseDTO> GetDashboardDataAsync();
        Task<IEnumerable<BestSellerBoxesDto>> GetBestSellerBoxForDashboard();

    }
}
