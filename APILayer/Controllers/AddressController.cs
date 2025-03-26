using Application.Services.Interfaces;
using Common.Model.Address.Request;
using Common.Model.Address.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Authorize(Roles = "User, Admin, Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto createAddressDTO)
        {
            var addressResponse = await _addressService.CreateAddressAsync(createAddressDTO);
            return Ok(addressResponse);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressDTO updateAddressDTO)
        {
            var addressResponse = await _addressService.UpdateAddressAsync(updateAddressDTO);
            return Ok(addressResponse);
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<AddressResponseDto>> GetAddressById(int addressId)
        {
            try
            {
                var addressResponse = await _addressService.GetAddressByIdAsync(addressId);
                if (addressResponse == null)
                {
                    return NotFound();
                }
                return Ok(addressResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressResponseDto>>> GetAllAddressesByUserIdOrGetAll(int userId)
        {
            var addresses = await _addressService.GetAllAddressesAsync(userId);
            return Ok(addresses);
        }

    }
}
