using Application.Services.Interfaces;
using Common.Model.BoxItemDTOs.Response;
using Common.Model.OnlineSerieBoxDTOs.Request;
using Common.Model.OnlineSerieBoxDTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Common.Exceptions.CustomExceptions;

namespace APILayer.Controllers
{
    [Route("api/online-serie-box")]
    [ApiController]
    public class OnlineSerieBoxController : ControllerBase
    {
        private readonly IOnlineSerieBoxService _onlineSerieBoxService;
        public OnlineSerieBoxController(IOnlineSerieBoxService onlineSerieBoxService)
        {
            _onlineSerieBoxService = onlineSerieBoxService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin, Staff")]
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
        [Authorize(Roles = "Admin, Staff")]
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
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetAllOnlineSerieBoxResponse>>> GetAllOnlineSerieBoxes()
        {
            var response = await _onlineSerieBoxService.GetAllOnlineSerieBoxesAsync();
            return Ok(response);
        }

        [HttpPost("unbox")]
        [Authorize(Roles = "User, Admin, Staff")]
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<OnlineSerieBoxDetailResponse>> GetOnlineSerieBoxById(int id)
        {
            try
            {
                var response = await _onlineSerieBoxService.GetOnlineSerieBoxByIdAsync(id);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}/publish")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<ActionResult> UpdatePublishStatus(int id, bool status)
        {
            try
            {
                bool response = await _onlineSerieBoxService.UpdatePublishStatusAsync(status, id);
                if(response == true)
                {
                    return Ok("Update publish status successfully");
                } else
                {
                    return BadRequest("Update publish status failed, out of stock");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
