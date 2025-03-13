using Application.Services.Interfaces;
using Common.Model.VoucherDTOs.Request;
using Common.Model.VoucherDTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherResponseDto>>> GetAllVouchers()
        {
            var vouchers = await _voucherService.GetAllVouchersAsync();
            return Ok(vouchers);
        }

        [HttpGet("{voucherId}")]
        public async Task<ActionResult<VoucherResponseDto>> GetVoucherById(int voucherId)
        {
            var voucher = await _voucherService.GetVoucherByIdAsync(voucherId);
            if (voucher == null)
            {
                return NotFound($"Voucher with ID {voucherId} not found.");
            }
            return Ok(voucher);
        }

        [HttpPost]
        public async Task<ActionResult<VoucherResponseDto>> CreateVoucher([FromBody] CreateVoucherRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request data is required.");
                }
                var createdVoucher = await _voucherService.CreateVoucherAsync(request);
                return CreatedAtAction(nameof(GetVoucherById), new { createdVoucher.voucherId }, createdVoucher);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{voucherId}")]
        public async Task<ActionResult<VoucherResponseDto>> UpdateVoucher(int voucherId, [FromBody] UpdateVoucherRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }

            var updatedVoucher = await _voucherService.UpdateVoucherAsync(voucherId, request);
            if (updatedVoucher == null)
            {
                return NotFound($"Voucher with ID {voucherId} not found.");
            }

            return Ok(updatedVoucher);
        }


        [HttpDelete("{voucherId}")]
        public async Task<ActionResult> DeleteVoucher(int voucherId)
        {
            var result = await _voucherService.DeleteVoucherAsync(voucherId);
            if (!result)
            {
                return NotFound($"Voucher with ID {voucherId} not found or already deleted.");
            }
            return NoContent();
        }
    }
}
