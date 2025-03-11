using Common.Model.VoucherDTOs.Request;
using Common.Model.VoucherDTOs.Response;

namespace Application.Services.Interfaces
{
    public interface IVoucherService
    {
        Task ReduceVoucherQuantity(int? voucherId);
        Task<VoucherResponseDto> GetVoucherByIdAsync(int voucherId);
        Task<IEnumerable<VoucherResponseDto>> GetAllVouchersAsync();
        Task<VoucherResponseDto> CreateVoucherAsync(CreateVoucherRequest request);
        Task<VoucherResponseDto> UpdateVoucherAsync(int voucherId, UpdateVoucherRequest request);
        Task<bool> DeleteVoucherAsync(int voucherId);
    }
}
