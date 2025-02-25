using BlindBoxSystem.Domain.Entities;

namespace BlindBoxSystem.Data.Repository.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllBlogAsync();

        Task<BlogPost> GetBlogByIdAsync(int id);
        Task<BlogPost> GetBlogByIdDTO(int id);
        Task<BlogPost> AddBlogAsync(BlogPost blogPost);
        Task<BlogPost> UpdateBlogAsync(BlogPost blogPost);
        Task DeleteBlogAsync(int id);
    }
}
