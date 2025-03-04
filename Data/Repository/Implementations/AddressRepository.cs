using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Domain.Domain.Model.Address.Request;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{

    public class AddressRepository : IAddressRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public AddressRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }
        public async Task<CreateAddressDto> AddAddress(CreateAddressDto createAddressDTO)
        {
            await _context.Addresses.AddAsync(new Address
            {
                AddressDetail = createAddressDTO.AddressDetail,
                District = createAddressDTO.District,
                Province = createAddressDTO.Province,
                Ward = createAddressDTO.Ward, 
                UserId = createAddressDTO.UserId,
                PhoneNumber = createAddressDTO.phoneNumber,
                Name = createAddressDTO.name,
                WardCode = createAddressDTO.WardCode,
                DistrictId = createAddressDTO.DistrictId,
                ProvinceId = createAddressDTO.ProvinceId,
                Note = createAddressDTO.note
            });
            await _context.SaveChangesAsync();
            return createAddressDTO;
        }

        public async Task<Address?> GetAddressByIdAsync(int addressId)
        {
            return await _context.Addresses.FindAsync(addressId);
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync(int userId)
        {
            if(userId > 0)
            {
                return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
            }
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> UpdateAddressAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return address;
        }
    }
}
