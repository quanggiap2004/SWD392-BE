using Common.Model.FeedbackDTOs.Request;
using Domain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<Feedback> CreateFeedback(Feedback feedback);
        Task<bool> DeleteFeedbackAsync(int id);
        Task<IEnumerable<Feedback>> GetAllFeedback();
        Task<IEnumerable<Feedback>> GetAllFeedbackByBoxId(int boxId);
        Task<bool> UpdateFeedback(int id, UpdateFeedbackRequestDto feedbackRequestDto);
    }
}
