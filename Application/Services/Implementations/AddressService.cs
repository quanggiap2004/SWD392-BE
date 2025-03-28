﻿using Application.Services.Interfaces;
using Common.Model.Address.Request;
using Common.Model.Address.Response;
using Data.Repository.Interfaces;

namespace Application.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<CreateAddressDto> CreateAddressAsync(CreateAddressDto createAddressDTO)
        {
            return await _addressRepository.AddAddress(createAddressDTO);
        }

        public async Task<AddressResponseDto> GetAddressByIdAsync(int id)
        {
            var address = await _addressRepository.GetAddressByIdAsync(id);
            if (address == null)
            {
                throw new KeyNotFoundException("Address not found");
            }

            return new AddressResponseDto
            {
                addressId = address.AddressId,
                province = address.Province,
                district = address.District,
                ward = address.Ward,
                addressDetail = address.AddressDetail,
                userId = address.UserId,
                phoneNumber = address.PhoneNumber,
                name = address.Name,
                wardCode = address.WardCode,
                districtId = address.DistrictId,
                provinceId = address.ProvinceId,
                note = address.Note
            };
        }

        public async Task<IEnumerable<AddressResponseDto>> GetAllAddressesAsync(int userId)
        {
            var addresses = await _addressRepository.GetAllAddressesAsync(userId);
            return addresses.Select(address => new AddressResponseDto
            {
                addressId = address.AddressId,
                province = address.Province,
                district = address.District,
                ward = address.Ward,
                wardCode = address.WardCode,
                addressDetail = address.AddressDetail,
                userId = address.UserId,
                phoneNumber = address.PhoneNumber,
                name = address.Name,
                districtId = address.DistrictId,
                provinceId = address.ProvinceId,
                note = address.Note
            });
        }

        public async Task<AddressResponseDto> UpdateAddressAsync(UpdateAddressDTO updateAddressDTO)
        {
            var address = await _addressRepository.GetAddressByIdAsync(updateAddressDTO.addressId);
            if (address == null)
            {
                throw new KeyNotFoundException("Address not found");
            }

            address.Province = updateAddressDTO.province;
            address.District = updateAddressDTO.district;
            address.Ward = updateAddressDTO.ward;
            address.AddressDetail = updateAddressDTO.addressDetail;
            address.PhoneNumber = updateAddressDTO.phoneNumber;
            address.Name = updateAddressDTO.name;
            address.WardCode = updateAddressDTO.WardCode;
            address.DistrictId = updateAddressDTO.DistrictId;
            address.ProvinceId = updateAddressDTO.ProvinceId;
            address.Note = updateAddressDTO.note;

            var updatedAddress = await _addressRepository.UpdateAddressAsync(address);
            return new AddressResponseDto
            {
                addressId = updatedAddress.AddressId,
                province = updatedAddress.Province,
                district = updatedAddress.District,
                ward = updatedAddress.Ward,
                addressDetail = updatedAddress.AddressDetail,
                userId = updatedAddress.UserId,
                name = updatedAddress.Name,
                phoneNumber = updatedAddress.PhoneNumber,
                districtId = address.DistrictId,
                provinceId = address.ProvinceId,
                wardCode = address.WardCode,
                note = address.Note
            };
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var address = await _addressRepository.GetAddressByIdAsync(addressId);
            if (address == null)
            {
                return false;
            }

            await _addressRepository.DeleteAddressAsync(address);
            return true;
        }
    }
}
