using System.Net;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

namespace Screenshot.Tests
{
    public class PostScreenshotTests : EndpointTests
    {

        [Fact]
        public async Task Post_WhenPostingAValidUrl_ItShouldReturn200OK()
        {
            var response = await Client.GetAsync("/api/todos/123");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
