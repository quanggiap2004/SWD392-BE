using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BlogPostDTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlindBoxSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        private readonly IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllBlogDTO>>> GetAllBlog()
        {
            var result = await _blogPostService.GetAllBlog();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogById(int id)
        {
            var blog = await _blogPostService.GetBlogById(id);

            if (blog == null)
            {
                return NotFound("Blog not found with " + id);
            }
            return blog;
        }

        [HttpGet("v2/{id}")]
        public async Task<ActionResult<GetAllBlogDTO>> GetBlogByIdWithDTO(int id)
        {
            var blog = await _blogPostService.GetBlogByIdDTO(id);

            if (blog == null)
            {
                return NotFound("Blog not found with " + id);
            }
            return blog;
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> AddBlog([FromBody] AddBlogDTO addBlogDTO)
        {

            if (addBlogDTO == null)
            {
                return BadRequest("Blog's Data is required");
            }

            //var existingBrand = await _brandService.GetBrandByNameAsync(addBrand.Name);
            //if (existingBrand != null)
            //{
            //    return Conflict("Brand already exists.");
            //}

            var ToAddBlog = new BlogPost
            {
                BlogPostTitle = addBlogDTO.BlogPostTitle,
                BlogPostContent = addBlogDTO.BlogPostContent,
                BlogPostImage = addBlogDTO.BlogPostImage,
            };

            var result = await _blogPostService.AddBlogAsync(ToAddBlog);
            return CreatedAtAction(nameof(GetBlogById), new { id = result.BlogPostId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBlog(int id)
        {
            var deletedBlog = await _blogPostService.GetBlogById(id);
            if (deletedBlog == null)
            {
                return NotFound("Blog not found with " + id);
            }
            await _blogPostService.DeleteBlogAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogPost>> UpdateBlog(int id, [FromBody] AddBlogDTO updateBlogDTO)
        {
            if (updateBlogDTO == null)
            {
                return BadRequest("Blog Data is required");
            }

            var blogToUpdate = new BlogPost
            {
                BlogPostId = id,
                BlogPostTitle = updateBlogDTO.BlogPostTitle,
                BlogPostContent = updateBlogDTO.BlogPostContent,
                BlogPostImage = updateBlogDTO.BlogPostImage,
            };

            var toUpdateBlogDTO = await _blogPostService.UpdateBlogAsync(id, blogToUpdate);

            if (updateBlogDTO == null)
            {
                return NotFound(new { message = "Blog not found." });
            }

            return Ok(new { message = "Blog updated successfully.", updateBlogDTO });
        }

    }
}
