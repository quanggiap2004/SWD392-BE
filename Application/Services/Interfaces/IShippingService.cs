using Common.Model.ShippingDTOs.Request;
using Common.Model.ShippingDTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IShippingService
    {
        Task<string> GetShopsAsync();
        Task<ShippingFeeResponseDTO> GetShippingFeeAsync(ShippingFeeRequestDTO shippingFeeRequest);
    }
}
