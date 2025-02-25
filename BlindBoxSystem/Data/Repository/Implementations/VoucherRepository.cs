using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;

namespace BlindBoxSystem.Data.Repository.Implementations
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        public VoucherRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }
        public async Task ReduceVoucherQuantity(int voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher == null)
            {
                throw new Exception("Voucher not found");
            }
            voucher.NumOfVoucher--;
            await _context.SaveChangesAsync();
        }
    }
}
