using Application.Services.Interfaces;
using Common.Model.BoxItemDTOs;
using Common.Model.UserVotedBoxItemDTOs;
using Domain.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User, Admin, Staff")]
    [ApiController]
    public class BoxItemController : ControllerBase
    {

        private readonly IBoxItemService _boxItemService;
        public BoxItemController(IBoxItemService boxItemService)
        {
            _boxItemService = boxItemService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetAllBoxItemDTO>>> GetAllBoxesItem()
        {
            var result = await _boxItemService.GetAllBoxItems();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<BoxItem>> AddBoxItem([FromBody] AddBoxItemDTO addBoxItemDTO)
        {
            try
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
                return CreatedAtAction(nameof(GetBoxItemById), new { id = result.boxItemId }, result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult> DeleteBoxItem(int id)
        {
            var deletedBoxItem = await _boxItemService.GetBoxItemById(id);
            if (deletedBoxItem == null)
            {
                return NotFound("Box Item not found with " + id);
            }
            var result = await _boxItemService.DeleteBoxItemAsync(id);
            if(result)
            {
                return Ok("Delete box item successfully");
            } else
            {
                return BadRequest("Delete box item failed. There is an online serie box that is published");
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
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


        [HttpPost("vote")]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<IActionResult> AddOrUpdateVote([FromBody] AddVoteDTO addVoteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _boxItemService.AddOrUpdateVoteAsync(addVoteDTO);
            return Ok(result);
        }

        [HttpGet("{boxItemId}/votes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVotesByBoxItemId(int boxItemId)
        {
            var votes = await _boxItemService.GetVotesByBoxItemId(boxItemId);
            if (votes == null)
            {
                return NotFound();
            }

            return Ok(votes);
        }
    }
}
