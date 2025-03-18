using Domain.Domain.Entities;

namespace Data.Repository.Interfaces
{
    public interface IVoucherRepository
    {
        Task<Voucher?> GetVoucherByIdAsync(int voucherId);
        Task<IEnumerable<Voucher>> GetAllVouchersAsync();
        Task<Voucher> CreateVoucherAsync(Voucher voucher);
        Task<Voucher> UpdateVoucherAsync(Voucher voucher);
        Task<bool> DeleteVoucherAsync(int voucherId);
        Task ReduceVoucherQuantity(int? voucherId);
    }
}
