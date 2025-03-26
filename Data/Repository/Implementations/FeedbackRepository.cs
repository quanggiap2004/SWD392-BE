using Common.Model.FeedbackDTOs.Request;
using Common.Model.FeedbackDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Context;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly BlindBoxSystemDbContext _context;
        private readonly IBoxOptionRepository _boxOptionRepository;
        private readonly IBoxRepository _boxRepository;
        public FeedbackRepository(BlindBoxSystemDbContext context, IBoxOptionRepository boxOptionRepository, IBoxRepository boxRepository)
        {
            _boxOptionRepository = boxOptionRepository;
            _boxRepository = boxRepository;
            _context = context;
        }
        public async Task<Feedback> CreateFeedback(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.OrderItems.Where(oi => oi.OrderItemId == feedback.OrderItemId).ExecuteUpdateAsync(setter => setter.SetProperty(oi => oi.IsFeedback, true));
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var orderItem = await _context.OrderItems.Where(oi => oi.Feedback.FeedbackId == id).FirstOrDefaultAsync();
            if (orderItem == null)
            {
                return false;
            }
            orderItem.IsFeedback = false;
            var result = await _context.Feedbacks.Where(f => f.FeedbackId == id).ExecuteDeleteAsync() > 0;
            await _context.SaveChangesAsync();
            await _boxOptionRepository.UpdateAverageBoxOptionRating(orderItem.BoxOptionId);
            await _boxRepository.UpdateBoxRatingByBoxOptionId(orderItem.BoxOptionId);
            return result;
        }

        public async Task<IEnumerable<FullFeedbackResponseDto>> GetAllFeedback()
        {
            return await _context.Feedbacks.AsNoTracking().Select( f => new FullFeedbackResponseDto
            {
                feedbackId = f.FeedbackId,
                feedbackContent = f.FeedbackContent,
                imageUrl = f.ImageUrl,
                rating = f.Rating,
                createdAt = f.CreatedAt,
                updatedAt = f.UpdatedAt,
                userId = f.User.UserId,
                userName = f.User.Username,
                email = f.User.Email,
                boxOptionName = f.OrderItem.BoxOption.BoxOptionName,
                boxOptionId = f.OrderItem.BoxOptionId,
                avatarUrl = f.User.AvatarUrl,
            }).ToListAsync();
        }

        public async Task<IEnumerable<FullFeedbackResponseDto>> GetAllFeedbackByBoxId(int boxId)
        {
            return await _context.Feedbacks.Where(f => f.OrderItem.BoxOption.BoxId == boxId).Select(f => new FullFeedbackResponseDto
            {
                feedbackId = f.FeedbackId,
                feedbackContent = f.FeedbackContent,
                imageUrl = f.ImageUrl,
                rating = f.Rating,
                createdAt = f.CreatedAt,
                updatedAt = f.UpdatedAt,
                userId = f.User.UserId,
                userName = f.User.Username,
                email = f.User.Email,
                boxOptionName = f.OrderItem.BoxOption.BoxOptionName,
                boxOptionId = f.OrderItem.BoxOptionId,
                avatarUrl = f.User.AvatarUrl,
            }).ToListAsync();
        }

        public async Task<bool> UpdateFeedback(int id, UpdateFeedbackRequestDto feedbackRequestDto)
        {
            var result = await _context.Feedbacks.Where(f => f.FeedbackId == id).ExecuteUpdateAsync(
                setter => setter.SetProperty(f => f.FeedbackContent, feedbackRequestDto.feedbackContent)
                .SetProperty(f => f.ImageUrl,feedbackRequestDto.imageUrl)
                .SetProperty(f => f.Rating, feedbackRequestDto.rating)
                .SetProperty(f => f.UpdatedAt, DateTime.UtcNow)) > 0;
            await _context.SaveChangesAsync();
            await _boxOptionRepository.UpdateAverageBoxOptionRating(feedbackRequestDto.boxOptionId);
            await _boxRepository.UpdateBoxRatingByBoxOptionId(feedbackRequestDto.boxOptionId);
            return result;
        }
    }
}
