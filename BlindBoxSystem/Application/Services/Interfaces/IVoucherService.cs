﻿
namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IVoucherService
    {
        Task ReduceVoucherQuantity(int voucherId);
    }
}
