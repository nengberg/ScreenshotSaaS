using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Screenshot.API.Features.Batch
{
    public class SubmitUrlBatchRequestHandler : IRequestHandler<SubmitUrlBatchRequest>
    {
        public Task<Unit> Handle(SubmitUrlBatchRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}