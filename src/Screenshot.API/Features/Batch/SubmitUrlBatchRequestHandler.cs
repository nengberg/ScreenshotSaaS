using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Screenshot.API.Infrastructure;

namespace Screenshot.API.Features.Batch
{
    public class SubmitUrlBatchRequestHandler : IRequestHandler<SubmitUrlBatchRequest>
    {
        private readonly IMessagePublisher _messagePublisher;

        public SubmitUrlBatchRequestHandler(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public Task<Unit> Handle(SubmitUrlBatchRequest request, CancellationToken cancellationToken)
        {
            foreach(var url in request.Urls)
            {
                _messagePublisher.Publish(url);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}