using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlindBoxSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {

        private readonly IBoxService _boxService;
        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllBoxesDTO>>> GetAllBoxes()
        {
            var result = await _boxService.GetAllBoxes();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBoxesById(int id)
        {
            var box = await _boxService.GetBoxById(id);
            if (box == null)
            {
                return NotFound("Box not found with " + id);
            }
            return box;
        }

        [HttpGet("withDTO/{id}")]
        public async Task<ActionResult<GetAllBoxesDTO>> GetBoxesByIdDTO(int id)
        {

            var box = await _boxService.GetBoxByIdDTO(id);
            if (box == null)
            {
                return NotFound("Box not found with " + id);
            }
            return box;
        }


        [HttpPost]
        public async Task<ActionResult<Box>> AddBox([FromBody] AddBoxDTO addBoxDTO)
        {

            if (addBoxDTO == null)
            {
                return BadRequest("Box's Data is required");
            }

            //var existingBrand = await _brandService.GetBrandByNameAsync(addBrand.Name);
            //if (existingBrand != null)
            //{
            //    return Conflict("Brand already exists.");
            //}

            var ToAddBox = new Box
            {
                BoxName = addBoxDTO.BoxName,
                BoxDescription = addBoxDTO.BoxDescription,
                BrandId = addBoxDTO.BrandId
            };

            var result = await _boxService.AddBoxAsync(ToAddBox);
            return CreatedAtAction(nameof(GetBoxesById), new { id = result.BoxId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBox(int id)
        {
            var deletedBox = await _boxService.GetBoxById(id);
            if (deletedBox == null)
            {
                return NotFound("Box not found with " + id);
            }
            await _boxService.DeleteBoxAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Box>> UpdateBox(int id, [FromBody] AddBoxDTO updateBoxDTO)
        {
            if (updateBoxDTO == null)
            {
                return BadRequest("Box's Data is required");
            }

            var boxToUpdate = new Box
            {
                BoxId = id,
                BoxName = updateBoxDTO.BoxName,
                BoxDescription = updateBoxDTO.BoxDescription,
                BrandId = updateBoxDTO.BrandId
            };

            var updatedBox = await _boxService.UpdateBoxAsync(id, boxToUpdate);

            if (updatedBox == null)
            {
                return NotFound(new { message = "Box not found." });
            }

            return Ok(new { message = "Box updated successfully.", updatedBox });
        }
    }
}
