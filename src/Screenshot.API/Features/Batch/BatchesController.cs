using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Screenshot.API.Features.Batch
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<string>>> SubmitUrls([FromBody]SubmitUrlBatchRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
