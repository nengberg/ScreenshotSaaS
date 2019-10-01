using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Screenshot.Infrastructure;

namespace Screenshot.API.Features.Batch
{
    public class SubmitUrlBatchRequestHandler : IRequestHandler<SubmitUrlBatchRequest>
    {
        private readonly IMessageBroker _messageBroker;

        public SubmitUrlBatchRequestHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public Task<Unit> Handle(SubmitUrlBatchRequest request, CancellationToken cancellationToken)
        {
            foreach(var url in request.Urls)
            {
                _messageBroker.Publish(new GenerateScreenshotMessage(url));
            }

            return Task.FromResult(Unit.Value);
        }
    }
}