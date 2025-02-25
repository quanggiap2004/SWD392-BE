﻿using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BoxImageDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlindBoxSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxImageController : ControllerBase
    {

        private readonly IBoxImageService _boxImageService;
        public BoxImageController(IBoxImageService boxImageService)
        {
            _boxImageService = boxImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllBoxImageDTO>>> GetAllBoxImages()
        {
            var result = await _boxImageService.GetAllBoxesImage();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BoxImage>> GetBoxImageById(int id)
        {
            var boxImage = await _boxImageService.GetBoxImageById(id);
            if (boxImage == null)
            {
                return NotFound("Box's image not found with " + id);
            }
            return boxImage;
        }

        [HttpGet("withDTO/{id}")]
        public async Task<ActionResult<GetAllBoxImageDTO>> GetBoxImageByIdDTO(int id)
        {

            var boxImage = await _boxImageService.GetBoxImageDTO(id);
            if (boxImage == null)
            {
                return NotFound("Box's image not found with " + id);
            }
            return boxImage;
        }


        [HttpPost]
        public async Task<ActionResult<BoxImage>> AddBoxImage([FromBody] AddBoxImageDTO addBoxImageDTO)
        {

            if (addBoxImageDTO == null)
            {
                return BadRequest("Box Image's Data is required");
            }

            //var existingBrand = await _brandService.GetBrandByNameAsync(addBrand.Name);
            //if (existingBrand != null)
            //{
            //    return Conflict("Brand already exists.");
            //}

            var ToAddBoxImage = new BoxImage
            {
                BoxImageUrl = addBoxImageDTO.BoxImageUrl,
                BoxId = addBoxImageDTO.BoxId,
            };

            var result = await _boxImageService.AddBoxImageAsync(ToAddBoxImage);
            return CreatedAtAction(nameof(GetBoxImageById), new { id = result.BoxImageId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoxImage(int id)
        {
            var deletedBoxImage = await _boxImageService.GetBoxImageById(id);
            if (deletedBoxImage == null)
            {
                return NotFound("Box Image not found with " + id);
            }
            await _boxImageService.DeleteBoxImageAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BoxImage>> UpdateBoxImage(int id, [FromBody] AddBoxImageDTO updateBoxImageDTO)
        {
            if (updateBoxImageDTO == null)
            {
                return BadRequest("Box Image's Data is required");
            }

            var boxImageToUpdate = new BoxImage
            {
                BoxImageId = id,
                BoxImageUrl = updateBoxImageDTO.BoxImageUrl,
                BoxId = updateBoxImageDTO.BoxId,
            };

            var updatedBoxImage = await _boxImageService.UpdateBoxImageAsync(id, boxImageToUpdate);

            if (updatedBoxImage == null)
            {
                return NotFound(new { message = "Box Image not found." });
            }

            return Ok(new { message = "Box Image updated successfully.", updatedBoxImage });
        }


    }
}
