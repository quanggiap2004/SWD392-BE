using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Context;
using BlindBoxSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlindBoxSystem.Data.Repository.Implementations
{
    public class BlogPostRepository : IBlogPostRepository


    {
        private readonly BlindBoxSystemDbContext _context;
        public BlogPostRepository(BlindBoxSystemDbContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> AddBlogAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task DeleteBlogAsync(int id)
        {
            var deleteBlog = await _context.BlogPosts.FindAsync(id);
            if (deleteBlog != null)
            {
                _context.BlogPosts.Remove(deleteBlog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogAsync()
        {
            return await _context.BlogPosts.Include(u => u.User).ToListAsync();
        }

        public async Task<BlogPost> GetBlogByIdAsync(int id)
        {
            return await _context.BlogPosts.FindAsync(id);
        }

        public async Task<BlogPost> GetBlogByIdDTO(int id)
        {
            return await _context.BlogPosts.Include(u => u.User).FirstOrDefaultAsync(b => b.BlogPostId == id);
        }

        public async Task<BlogPost> UpdateBlogAsync(BlogPost blogPost)
        {
            _context.BlogPosts.Update(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }
    }
}
