using Application.Services.Interfaces;
using Domain.Domain.Model.OnlineSerieBoxDTOs.Request;
using Domain.Domain.Model.OnlineSerieBoxDTOs.Response;
using Microsoft.AspNetCore.Mvc;
using static Common.Exceptions.CustomExceptions;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineSerieBoxController : ControllerBase
    {
        private readonly IOnlineSerieBoxService _onlineSerieBoxService;
        public OnlineSerieBoxController(IOnlineSerieBoxService onlineSerieBoxService)
        {
            _onlineSerieBoxService = onlineSerieBoxService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateBoxOptionAndOnlineSerieBoxResponse>> CreateBoxOptionAndOnlineSerieBox([FromBody] CreateBoxOptionAndOnlineSerieBoxRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }

            try
            {
                var response = await _onlineSerieBoxService.CreateBoxOptionAndOnlineSerieBoxAsync(request);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateOnlineSerieBoxResponse>> UpdateOnlineSerieBox(int id, [FromBody] UpdateOnlineSerieBoxRequest request)
        {
            try
            {
                var updatedBox = await _onlineSerieBoxService.UpdateOnlineSerieBoxAsync(id, request);
                if (updatedBox == null)
                {
                    return NotFound();
                }
                return Ok(updatedBox);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }  
        }


    }
}
