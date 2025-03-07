using Common.Model.Address.Request;
using Common.Model.Address.Response;

namespace Application.Services.Interfaces
{
    public interface IAddressService
    {
        Task<CreateAddressDto> CreateAddressAsync(CreateAddressDto createAddressDTO);
        Task<AddressResponseDto> GetAddressByIdAsync(int id);
        Task<IEnumerable<AddressResponseDto>> GetAllAddressesAsync(int userId);
        Task<AddressResponseDto> UpdateAddressAsync(UpdateAddressDTO updateAddressDTO);
    }
}
