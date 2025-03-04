﻿using Application.Services.Interfaces;
using Data.Repository.Interfaces;

namespace Application.Services.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        public async Task ReduceVoucherQuantity(int voucherId)
        {
            await _voucherRepository.ReduceVoucherQuantity(voucherId);
        }
    }
}
