using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Implementations
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public VoucherRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }
        public async Task ReduceVoucherQuantity(int? voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher == null)
            {
                return;
            }
            voucher.NumOfVoucher--;
            await _context.SaveChangesAsync();
        }

        public async Task<Voucher> CreateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }

        public async Task<bool> DeleteVoucherAsync(int voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher == null || voucher.IsDeleted)
            {
                return false;
            }

            voucher.IsDeleted = true;
            _context.Vouchers.Update(voucher);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
        {
            var currentDate = DateTime.UtcNow.Date;
            return await _context.Vouchers
                                 .Where(v => !v.IsDeleted &&
                                             v.VoucherStartDate.Date <= currentDate &&
                                             v.VoucherEndDate.Date >= currentDate)
                                 .ToListAsync();
        }

        public async Task<Voucher?> GetVoucherByIdAsync(int voucherId)
        {
            var currentDate = DateTime.UtcNow.Date;
            return await _context.Vouchers
                                 .FirstOrDefaultAsync(v => v.VoucherId == voucherId &&
                                                          !v.IsDeleted &&
                                                          v.VoucherStartDate.Date <= currentDate &&
                                                          v.VoucherEndDate.Date >= currentDate);
        }

        public async Task<Voucher> UpdateVoucherAsync(Voucher voucher)
        {
            var existingVoucher = await _context.Vouchers.FindAsync(voucher.VoucherId);
            if (existingVoucher == null || existingVoucher.IsDeleted)
            {
                return null;
            }

            existingVoucher.VoucherName = voucher.VoucherName;
            existingVoucher.VoucherCode = voucher.VoucherCode;
            existingVoucher.VoucherDiscount = voucher.VoucherDiscount;
            existingVoucher.VoucherStartDate = voucher.VoucherStartDate;
            existingVoucher.VoucherEndDate = voucher.VoucherEndDate;
            existingVoucher.MaxDiscount = voucher.MaxDiscount;

            _context.Vouchers.Update(existingVoucher);
            await _context.SaveChangesAsync();
            return existingVoucher;
        }
    }
}
