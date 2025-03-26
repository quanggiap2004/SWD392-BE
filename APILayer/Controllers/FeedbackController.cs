using Application.Services.Interfaces;
using Common.Model.FeedbackDTOs.Request;
using Common.Model.FeedbackDTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult<FeedbackResponseDto>> CreateFeedback([FromBody] FeedbackRequestDto feedbackRequestDto)
        {
            try
            {
                var result = await _feedbackService.CreateFeedback(feedbackRequestDto);
                return Ok(result);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FullFeedbackResponseDto>>> GetAllFeedback()
        {
            var feedbacks = await _feedbackService.GetAllFeedback();
            return Ok(feedbacks);
        }

        [HttpGet("boxes/{boxId}/feedback")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FullFeedbackResponseDto>>> GetAllFeedbackByBoxId(int boxId)
        {
            var feedbacks = await _feedbackService.GetAllFeedbackByBoxId(boxId);
            return Ok(feedbacks);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User, Admin, Staff")]
        public async Task<ActionResult<FeedbackResponseDto>> UpdateFeedback(int id, [FromBody] UpdateFeedbackRequestDto feedbackRequestDto)
        {
            try
            {
                var result = await _feedbackService.UpdateFeedback(id, feedbackRequestDto);
                return Ok("Update feedback successfully");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult> DeleteFeedbackAsync(int id)
        {
            try
            {
                var result = await _feedbackService.DeleteFeedbackAsync(id);
                return Ok("Delete successfully");
            }
            catch (Exception e)
            {
                return NotFound("Delete fail, can't find the feedback with id:" + id);
            }
        }
        
    }
}
