using BlindBoxSystem.Application.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxItemDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlindBoxSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxItemController : ControllerBase
    {

        private readonly IBoxItemService _boxItemService;
        public BoxItemController(IBoxItemService boxItemService)
        {
            _boxItemService = boxItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllBoxItemDTO>>> GetAllBoxesItem()
        {
            var result = await _boxItemService.GetAllBoxItems();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoxItem>> GetBoxItemById(int id)
        {
            var boxItem = await _boxItemService.GetBoxItemById(id);
            if (boxItem == null)
            {
                return NotFound("Box's Item not found with " + id);
            }
            return boxItem;
        }

        [HttpGet("withDTO/{id}")]
        public async Task<ActionResult<GetAllBoxItemDTO>> GetBoxItemByIdDTO(int id)
        {

            var boxItem = await _boxItemService.GetBoxItemDTO(id);
            if (boxItem == null)
            {
                return NotFound("Box's Item not found with " + id);
            }
            return boxItem;
        }


        [HttpPost]
        public async Task<ActionResult<BoxItem>> AddBoxItem([FromBody] AddBoxItemDTO addBoxItemDTO)
        {

            if (addBoxItemDTO == null)
            {
                return BadRequest("Box Item's Data is required");
            }

            //var existingBrand = await _brandService.GetBrandByNameAsync(addBrand.Name);
            //if (existingBrand != null)
            //{
            //    return Conflict("Brand already exists.");
            //}

            var ToAddBoxItem = new BoxItem
            {
                BoxItemName = addBoxItemDTO.BoxItemName,
                BoxItemDescription = addBoxItemDTO.BoxItemDescription,
                BoxItemColor = addBoxItemDTO.BoxItemColor,
                BoxItemEyes = addBoxItemDTO.BoxItemEyes,
                ImageUrl = addBoxItemDTO.ImageUrl,
                IsSecret = addBoxItemDTO.IsSecret,
                BoxId = addBoxItemDTO.BoxId,
            };

            var result = await _boxItemService.AddBoxItemAsync(ToAddBoxItem);
            return CreatedAtAction(nameof(GetBoxItemById), new { id = result.BoxItemId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoxItem(int id)
        {
            var deletedBoxItem = await _boxItemService.GetBoxItemById(id);
            if (deletedBoxItem == null)
            {
                return NotFound("Box Item not found with " + id);
            }
            await _boxItemService.DeleteBoxItemAsync(id);
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BoxItem>> UpdateBoxItem(int id, [FromBody] AddBoxItemDTO updateBoxItemDTO)
        {
            if (updateBoxItemDTO == null)
            {
                return BadRequest("Box Item's Data is required");
            }

            var boxItemToUpdate = new BoxItem
            {
                BoxItemId = id,
                BoxItemName = updateBoxItemDTO.BoxItemName,
                BoxItemDescription = updateBoxItemDTO.BoxItemDescription,
                BoxItemColor = updateBoxItemDTO.BoxItemColor,
                BoxItemEyes = updateBoxItemDTO.BoxItemEyes,
                ImageUrl = updateBoxItemDTO.ImageUrl,
                IsSecret = updateBoxItemDTO.IsSecret,
                BoxId = updateBoxItemDTO.BoxId,
            };

            var updatedBoxItem = await _boxItemService.UpdateBoxItemAsync(id, boxItemToUpdate);

            if (updatedBoxItem == null)
            {
                return NotFound(new { message = "Box Item not found." });
            }

            return Ok(new { message = "Box Item updated successfully.", updatedBoxItem });
        }
    }
}
