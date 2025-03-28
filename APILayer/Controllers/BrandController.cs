using Application.Services.Interfaces;
using Common.Model.BrandDTOs;
using Domain.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<ActionResult<IEnumerable<GetAllBrandsDTO>>> GetAllBrands()
        {
            var result = await _brandService.GetAllBrands();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Brand>> GetBrandsById(int id)
        {
            return await _brandService.GetBrandById(id);
        }

        [HttpGet("WithBoxName/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetAllBrandsDTO>> GetBrandWithBoxName(int id)
        {
            return await _brandService.GetBrandWithBoxName(id);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<Brand>> AddBrand([FromBody] AddBrandDTO addBrandDTO)
        {

            if (addBrandDTO == null)
            {
                return BadRequest("Brand's Data is required");
            }

            //var existingBrand = await _brandService.GetBrandByNameAsync(addBrand.Name);
            //if (existingBrand != null)
            //{
            //    return Conflict("Brand already exists.");
            //}

            var ToAddBrand = new Brand
            {
                BrandName = addBrandDTO.BrandName,
                ImageUrl = addBrandDTO.ImageUrl,
            };

            var result = await _brandService.AddBrandAsync(ToAddBrand);
            return CreatedAtAction(nameof(GetBrandsById), new { id = result.BrandId }, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            try
            {
                var deletedBrand = await _brandService.GetBrandById(id);
                if (deletedBrand == null)
                {
                    return NotFound("Brand not found with " + id);
                }
                await _brandService.DeleteBrandAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult<Brand>> UpdateBrand(int id, [FromBody] AddBrandDTO updateBrandDTO)
        {
            if (updateBrandDTO == null)
            {
                return BadRequest(new { message = "Brand's Data is required" });
            }

            var brandToUpdate = new Brand
            {
                BrandId = id,
                BrandName = updateBrandDTO.BrandName,
                ImageUrl = updateBrandDTO.ImageUrl,
            };

            var updatedBrand = await _brandService.UpdateBrandAsync(id, brandToUpdate);

            if (updatedBrand == null)
            {
                return NotFound(new { message = "Brand not found." });
            }

            return Ok(new { message = "Brand updated successfully.", updatedBrand });
        }

    }
}
