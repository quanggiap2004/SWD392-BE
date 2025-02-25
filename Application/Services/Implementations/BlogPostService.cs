﻿using BlindBoxSystem.Application.Services.Interfaces;
using BlindBoxSystem.Data.Repository.Interfaces;
using BlindBoxSystem.Domain.Entities;
using BlindBoxSystem.Domain.Model.BlogPostDTOs;

namespace BlindBoxSystem.Application.Services.Implementations
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<BlogPost> AddBlogAsync(BlogPost blogPost)
        {
            var addedBlog = await _blogPostRepository.AddBlogAsync(blogPost);
            return addedBlog;
        }

        public async Task DeleteBlogAsync(int id)
        {
            var existingBlog = await _blogPostRepository.GetBlogByIdAsync(id);

            if (existingBlog != null)
            {
                await _blogPostRepository.DeleteBlogAsync(id);
            }
        }

        public async Task<IEnumerable<GetAllBlogDTO>> GetAllBlog()
        {
            var blog = await _blogPostRepository.GetAllBlogAsync();
            var blogDTO = blog.Select(blog => new GetAllBlogDTO
            {
                BlogPostId = blog.BlogPostId,
                BlogPostTitle = blog.BlogPostTitle,
                BlogPostContent = blog.BlogPostContent,
                BlogPostImage = blog.BlogPostImage,
                Author = new AuthorDTO
                {
                    UserId = blog.User.UserId,
                    Username = blog.User.Username,
                    Email = blog.User.Email,
                    Fullname = blog.User.Fullname,
                    Phone = blog.User.Phone,
                    RoleId = blog.User.RoleId
                }
            });
            return blogDTO;
        }

        public async Task<BlogPost> GetBlogById(int id)
        {
            var blog = await _blogPostRepository.GetBlogByIdAsync(id);
            return blog;
        }

        public async Task<GetAllBlogDTO> GetBlogByIdDTO(int id)
        {
            var blog = await _blogPostRepository.GetBlogByIdDTO(id);
            var blogDTO = new GetAllBlogDTO
            {
                BlogPostId = blog.BlogPostId,
                BlogPostTitle = blog.BlogPostTitle,
                BlogPostContent = blog.BlogPostContent,
                BlogPostImage = blog.BlogPostImage,
                Author = new AuthorDTO
                {
                    UserId = blog.User.UserId,
                    Username = blog.User.Username,
                    Email = blog.User.Email,
                    Fullname = blog.User.Fullname,
                    Phone = blog.User.Phone,
                    RoleId = blog.User.RoleId
                }
            };
            return blogDTO;
        }

        public async Task<BlogPost> UpdateBlogAsync(int id, BlogPost blogPost)
        {
            var existingBlog = await _blogPostRepository.GetBlogByIdAsync(id);
            if (existingBlog == null)
            {
                return null; // Return null if the brand does not exist
            }
            existingBlog.BlogPostTitle = blogPost.BlogPostTitle;
            existingBlog.BlogPostContent = blogPost.BlogPostContent;
            existingBlog.BlogPostImage = blogPost.BlogPostImage;

            return await _blogPostRepository.UpdateBlogAsync(existingBlog);
        }
    }
}
