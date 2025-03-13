using Common.Model.FeedbackDTOs.Request;
using Common.Model.FeedbackDTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackResponseDto> CreateFeedback(FeedbackRequestDto feedbackRequestDto);
        Task<bool> DeleteFeedbackAsync(int id);
        Task<IEnumerable<FullFeedbackResponseDto>> GetAllFeedback();
        Task<IEnumerable<FullFeedbackResponseDto>> GetAllFeedbackByBoxId(int boxOptionId);
        Task<bool> UpdateFeedback(int id, UpdateFeedbackRequestDto feedbackRequestDto);
    }
}
