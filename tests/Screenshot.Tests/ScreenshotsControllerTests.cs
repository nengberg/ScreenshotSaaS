using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using NSubstitute;

using Screenshot.API.Features.Screenshot;

using Shouldly;

using Xunit;


namespace Screenshot.Tests
{
    public class ScreenshotsControllerTests : EndpointTests
    {
        private readonly List<Domain.Screenshot> _screenshots;

        public ScreenshotsControllerTests()
        {
            _screenshots = new List<Domain.Screenshot>();
        }

        [Fact]
        public async Task GivenGetToScreenshots_NoScreenshotsReturned_AnEmptyResponseShouldBeReturned()
        {
            GetScreenshotsQuery.Execute().Returns(_screenshots);

            var response = await Client.GetAsync("/api/screenshots");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var screenshotResponse = await DeserializeResponse<ScreenshotsResponse>(response);
            screenshotResponse.Screenshots.ShouldBeEmpty();
        }

        [Fact]
        public async Task GivenGetToScreenshots_ScreenshotsReturned_ResponseShouldContainScreenshots()
        {
            _screenshots.Add(new Domain.Screenshot { Data = new byte[] { 1 } });
            _screenshots.Add(new Domain.Screenshot { Data = new byte[] { 2 } });
            GetScreenshotsQuery.Execute().Returns(_screenshots);

            var response = await Client.GetAsync("/api/screenshots");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var screenshotResponse = await DeserializeResponse<ScreenshotsResponse>(response);
            screenshotResponse.Screenshots.Count().ShouldBe(_screenshots.Count);
        }
    }
}
