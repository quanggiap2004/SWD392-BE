using Data.Repository.Interfaces;
using Domain.Domain.Context;

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
    }
}
