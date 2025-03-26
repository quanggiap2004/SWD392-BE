using Application.Services.Interfaces;
using Common.Model.BoxOptionDTOs;
using Common.Model.BoxOptionDTOs.Request;
using Domain.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxOptionController : ControllerBase
    {

        private readonly IBoxOptionService _boxOptionService;
        public BoxOptionController(IBoxOptionService boxOptionService)
        {
            _boxOptionService = boxOptionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetAllBoxOptionDTO>>> GetAllBoxOptions()
        {
            var result = await _boxOptionService.GetAllBoxOptions();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<BoxOption>> GetBoxOptionById(int id)
        {
            var boxOption = await _boxOptionService.GetBoxOptionById(id);
            if (boxOption == null)
            {
                return NotFound("Box's Option not found with " + id);
            }
            return boxOption;
        }


        [HttpGet("withDTO/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetAllBoxOptionDTO>> GetBoxOptionByIdDTO(int id)
        {

            var boxOption = await _boxOptionService.GetBoxOptionDTO(id);
            if (boxOption == null)
            {
                return NotFound("Box's Option not found with " + id);
            }
            return boxOption;
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<BoxOption>> AddBoxOption([FromBody] AddBoxOptionDTO addBoxOptionDTO)
        {

            if (addBoxOptionDTO == null)
            {
                return BadRequest("Box Option's Data is required");
            }

            var ToAddBoxOption = new BoxOption
            {
                BoxOptionName = addBoxOptionDTO.BoxOptionName,
                BoxOptionStock = addBoxOptionDTO.BoxOptionStock,
                OriginPrice = addBoxOptionDTO.OriginPrice,
                DisplayPrice = addBoxOptionDTO.DisplayPrice,
                IsDeleted = addBoxOptionDTO.IsDeleted,
                BoxId = addBoxOptionDTO.BoxId,
            };

            var result = await _boxOptionService.AddBoxOptionAsync(ToAddBoxOption);
            return CreatedAtAction(nameof(GetBoxOptionById), new { id = result.BoxOptionId }, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult> DeleteBoxOption(int id)
        {
            var deletedBoxOption = await _boxOptionService.GetBoxOptionById(id);
            if (deletedBoxOption == null)
            {
                return NotFound("Box Option not found with " + id);
            }
            await _boxOptionService.DeleteBoxOptionAsync(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<BoxOption>> UpdateBoxOption(int id, [FromBody] AddBoxOptionDTO updateBoxOptionDTO)
        {
            if (updateBoxOptionDTO == null)
            {
                return BadRequest("Box Option's Data is required");
            }

            var boxOptionToUpdate = new BoxOption
            {
                BoxOptionId = id,
                BoxOptionName = updateBoxOptionDTO.BoxOptionName,
                BoxOptionStock = updateBoxOptionDTO.BoxOptionStock,
                OriginPrice = updateBoxOptionDTO.OriginPrice,
                DisplayPrice = updateBoxOptionDTO.DisplayPrice,
                IsDeleted = updateBoxOptionDTO.IsDeleted,
                BoxId = updateBoxOptionDTO.BoxId,
            };

            var updatedBoxOption = await _boxOptionService.UpdateBoxOptionAsync(id, boxOptionToUpdate);

            if (updatedBoxOption == null)
            {
                return NotFound(new { message = "Box Option not found." });
            }

            return Ok(new { message = "Box Option updated successfully.", updatedBoxOption });
        }
    }
}
