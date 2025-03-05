using Domain.Domain.Model.ShippingDTOs.Request;
using Domain.Domain.Model.ShippingDTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IShippingService
    {
        Task<string> GetShopsAsync();
        Task<ShippingFeeResponseDTO> GetShippingFeeAsync(ShippingFeeRequestDTO shippingFeeRequest);
    }
}
