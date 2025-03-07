using Common.Model.Address.Request;
using Domain.Domain.Entities;

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
