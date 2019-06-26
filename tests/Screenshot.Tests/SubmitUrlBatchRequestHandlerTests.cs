using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using NSubstitute;

using Screenshot.API.Features.Batch;
using Screenshot.Infrastructure;

using Xunit;

namespace Screenshot.Tests
{
    public class SubmitUrlBatchRequestHandlerTests
    {
        private readonly SubmitUrlBatchRequestHandler _sut;
        private readonly IMessageBroker _messageBroker;
        private readonly SubmitUrlBatchRequest _request;

        public SubmitUrlBatchRequestHandlerTests()
        {
            _messageBroker = Substitute.For<IMessageBroker>();
            _sut = new SubmitUrlBatchRequestHandler(_messageBroker);
            _request = new SubmitUrlBatchRequest();
        }

        [Fact]
        public async Task Handle_RequestContainsNoUrls_NoCallsToMessageBrokerPublishShouldBeDone()
        {
            _request.Urls = new string[0];

            await _sut.Handle(_request, CancellationToken.None);

            _messageBroker.Received(0).Publish(Arg.Any<GenerateScreenshotMessage>());
        }

        [Fact]
        public async Task Handle_RequestContainsUrls_OneCallPerUrlShouldBeMadeToMessageBrokerPublish()
        {
            _request.Urls = new[] { "http://abc.se", "https://def.ghi" };

            await _sut.Handle(_request, CancellationToken.None);

            _messageBroker
                .Received(_request.Urls.Count())
                .Publish(Arg.Any<GenerateScreenshotMessage>());
        }
    }
}