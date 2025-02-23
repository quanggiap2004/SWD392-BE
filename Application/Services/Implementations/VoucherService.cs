using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Data.Repository.Interfaces;

namespace BlindBoxSystem.Application.Services.Implementations
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
