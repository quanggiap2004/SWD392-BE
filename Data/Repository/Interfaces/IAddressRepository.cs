using Domain.Domain.Entities;
using Domain.Domain.Model.Address.Request;
using Domain.Domain.Model.Address.Response;

namespace Data.Repository.Interfaces
{
    public interface IAddressRepository
    {
        Task<CreateAddressDto> AddAddress(CreateAddressDto createAddressDTO);
        Task<Address?> GetAddressByIdAsync(int addressId);
        Task<IEnumerable<Address>> GetAllAddressesAsync(int userId);
        Task<Address> UpdateAddressAsync(Address address);
    }
}
