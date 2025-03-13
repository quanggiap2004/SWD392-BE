using Application.Services.Interfaces;
using AutoMapper;
using Common.Model.Address.Request;
using Common.Model.FeedbackDTOs.Request;
using Common.Model.FeedbackDTOs.Response;
using Data.Repository.Interfaces;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class FeedbackService :  IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly IBoxOptionService _boxOptionService;
        private readonly IBoxService _boxService;
        private readonly IOrderItemService _orderItemService;
        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper, IBoxOptionService boxOptionService, IBoxService boxService, IOrderItemService orderItemService)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _boxOptionService = boxOptionService;
            _boxService = boxService;
            _orderItemService = orderItemService;
        }
        public async Task<FeedbackResponseDto> CreateFeedback(FeedbackRequestDto feedbackRequestDto)
        {
            var orderItem = await _orderItemService.GetOrderItemById(feedbackRequestDto.orderItemId);
            if(orderItem == null)
            {
                throw new Exception("Order item not found");
            } else if(orderItem.IsFeedback == true)
            {
                throw new Exception("Order item already has feedback");
            }

            try
            {
                var feedback = _mapper.Map<Feedback>(feedbackRequestDto);
                var addResult = await _feedbackRepository.CreateFeedback(feedback);
                var feedbackResonseDto = _mapper.Map<FeedbackResponseDto>(addResult);
                bool updateBoxOptionResult = await _boxOptionService.UpdateAverageBoxOptionRating(feedbackRequestDto.boxOptionId);
                if (updateBoxOptionResult == false)
                {
                    throw new Exception("Update average box option rating failed");
                }
                bool updateBoxResult = await _boxService.UpdateBoxRatingByBoxOptionId(feedbackRequestDto.boxOptionId);
                if (updateBoxResult == false)
                {
                    throw new Exception("Update average box rating failed");
                }
                return feedbackResonseDto;
            }
            catch(DbUpdateException e)
            {
                throw new Exception("Update order item failed, item already have feedback");
            }
            
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var result = await _feedbackRepository.DeleteFeedbackAsync(id);
            if(result == false)
            {
                throw new Exception("Delete feedback failed");
            }
            return result;
        }

        public async Task<IEnumerable<FullFeedbackResponseDto>> GetAllFeedback()
        {
            var feedbacks = await _feedbackRepository.GetAllFeedback();
            return _mapper.Map<IEnumerable<FullFeedbackResponseDto>>(feedbacks);
        }

        public async Task<IEnumerable<FullFeedbackResponseDto>> GetAllFeedbackByBoxId(int boxId)
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbackByBoxId(boxId);
            return _mapper.Map<IEnumerable<FullFeedbackResponseDto>>(feedbacks);
        }

        public async Task<bool> UpdateFeedback(int id, UpdateFeedbackRequestDto feedbackRequestDto)
        {
            var result = await _feedbackRepository.UpdateFeedback(id, feedbackRequestDto);
            if (result == false)
            {
                throw new Exception("Update feedback failed");
            }
            return result;
        }
    }
}
