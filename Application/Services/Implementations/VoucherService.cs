using Application.Services.Interfaces;
using AutoMapper;
using Common.Exceptions;
using Common.Model.VoucherDTOs.Request;
using Common.Model.VoucherDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using System;

namespace Application.Services.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;
        public VoucherService(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }
        public async Task ReduceVoucherQuantity(int? voucherId)
        {
            if (voucherId == null || voucherId == 0)
            {
                return;
            }
            await _voucherRepository.ReduceVoucherQuantity(voucherId);
        }

        public async Task<VoucherResponseDto> CreateVoucherAsync(CreateVoucherRequest request)
        {
            try
            {
                var voucher = _mapper.Map<Voucher>(request);
                voucher.VoucherCode = GenerateVoucherCode(9);
                var createdVoucher = await _voucherRepository.CreateVoucherAsync(voucher);
                return _mapper.Map<VoucherResponseDto>(createdVoucher);
            }
            catch (Exception ex)
            {
                throw new CustomExceptions.BadRequestException("Voucher id create failed due to random into duplicate id :((");
            }
        }

        public async Task<bool> DeleteVoucherAsync(int voucherId)
        {
            return await _voucherRepository.DeleteVoucherAsync(voucherId);
        }

        public async Task<IEnumerable<VoucherResponseDto>> GetAllVouchersAsync()
        {
            var vouchers = await _voucherRepository.GetAllVouchersAsync();
            return _mapper.Map<IEnumerable<VoucherResponseDto>>(vouchers);
        }

        public async Task<VoucherResponseDto> GetVoucherByIdAsync(int voucherId)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            if(voucher == null)
            {
                throw new CustomExceptions.NotFoundException($"Voucher with id {voucherId} not found");
            }
            return _mapper.Map<VoucherResponseDto>(voucher);
        }

        public async Task<VoucherResponseDto> UpdateVoucherAsync(int voucherId, UpdateVoucherRequest request)
        {
            var voucherToUpdate = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            if (voucherToUpdate == null)
            {
                return null;
            }

            _mapper.Map(request, voucherToUpdate);
            var updatedVoucher = await _voucherRepository.UpdateVoucherAsync(voucherToUpdate);
            return _mapper.Map<VoucherResponseDto>(updatedVoucher);
        }
        private string GenerateVoucherCode(int length)
        {
            Random _random = new Random();
            string VoucherChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, length)
                .Select(_ => VoucherChars[_random.Next(VoucherChars.Length)])
                .ToArray());
        }
    }
}
