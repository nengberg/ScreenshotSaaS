using System.Net;
using System.Threading.Tasks;

using Screenshot.API.Features.Batch;

using Shouldly;

using Xunit;

namespace Screenshot.Tests
{
    public class SubmitBatchesTests : EndpointTests
    {
        [Fact]
        public async Task GivenPostToBatches_WhenPostingInvalidUrls_BadRequestShouldBeReturned()
        {
            var payload = new SubmitUrlBatchRequest();
            payload.Urls = new[] { "abc", "def" };
            var content = CreateStringContent(payload);

            var response = await Client.PostAsync("/api/batches", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenPostToBatches_WhenPostingEmptyRequest_BadRequestShouldBeReturned()
        {
            var payload = new SubmitUrlBatchRequest();
            payload.Urls = new string[0];
            var content = CreateStringContent(payload);

            var response = await Client.PostAsync("/api/batches", content);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenPostToBatches_WhenPostingValidUrls_ReturnStatusShouldBeOK()
        {
            var payload = new SubmitUrlBatchRequest();
            payload.Urls = new [] { "http://abc.se", "https://def.ghi" };
            var content = CreateStringContent(payload);

            var response = await Client.PostAsync("/api/batches", content);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }
    }
}
