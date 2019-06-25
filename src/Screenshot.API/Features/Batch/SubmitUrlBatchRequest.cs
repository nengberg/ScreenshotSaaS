using System.Collections.Generic;

using MediatR;

namespace Screenshot.API.Features.Batch
{
    public class SubmitUrlBatchRequest : IRequest
    {
        public IEnumerable<string> Urls { get; set; }
    }
}