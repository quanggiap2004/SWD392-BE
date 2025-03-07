using Application.Services.Interfaces;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.OnlineSerieBoxDTOs.Request;
using Common.Model.OnlineSerieBoxDTOs.Response;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllOnlineSerieBoxResponse>>> GetAllOnlineSerieBoxes()
        {
            var response = await _onlineSerieBoxService.GetAllOnlineSerieBoxesAsync();
            return Ok(response);
        }

        [HttpPost("unbox")]
        public async Task<ActionResult<BoxItemResponseDto>> OpenOnlineSerieBox([FromBody] OpenOnlineSerieBoxRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request data is required.");
            }
            try
            {
                BoxItemResponseDto response = await _onlineSerieBoxService.OpenOnlineSerieBoxAsync(request);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
