using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Screenshot.API.Features.Screenshot
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenshotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScreenshotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ScreenshotsResponse>> GetScreenshots()
        {
            var response = await _mediator.Send(new GetScreenshotsRequest());
            return Ok(response);
        }
    }
}
