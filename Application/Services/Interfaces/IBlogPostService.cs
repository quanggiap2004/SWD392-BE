﻿using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BlogPostDTOs;

namespace BlindBoxSystem.Application.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task<IEnumerable<GetAllBlogDTO>> GetAllBlog();
        Task<BlogPost> GetBlogById(int id);
        Task<GetAllBlogDTO> GetBlogByIdDTO(int id);
        Task<BlogPost> AddBlogAsync(BlogPost blogPost);
        Task<BlogPost> UpdateBlogAsync(int id, BlogPost blogPost);
        Task DeleteBlogAsync(int id);
    }
}
