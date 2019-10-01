using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using NSubstitute;

using Screenshot.API.Features.Screenshot;

using Shouldly;

using Xunit;
using Screenshot = Screenshot.Domain.Screenshot;


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
        public async Task GivenGetToScreenshots_WhenNoScreenshotsReturned_AnEmptyResponseShouldBeReturned()
        {
            GetScreenshotsQuery.Execute(CancellationToken.None).Returns(_screenshots);

            var response = await Client.GetAsync("/api/screenshots");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var screenshotResponse = await DeserializeResponse<ScreenshotsResponse>(response);
            screenshotResponse.Screenshots.ShouldBeEmpty();
            screenshotResponse.Count.ShouldBe(0);
        }

        [Fact]
        public async Task GivenGetToScreenshots_WhenScreenshotsReturned_ResponseShouldContainScreenshots()
        {
            _screenshots.Add(CreateScreenshot(new byte[] { 1 }, "http://abc.se"));
            _screenshots.Add(CreateScreenshot(new byte[] { 2 }, "http://def.gh"));
            GetScreenshotsQuery.Execute(CancellationToken.None).Returns(_screenshots);

            var response = await Client.GetAsync("/api/screenshots");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var screenshotResponse = await DeserializeResponse<ScreenshotsResponse>(response);
            var screenshots = screenshotResponse.Screenshots.ToList();
            screenshots.Count.ShouldBe(_screenshots.Count);
            screenshots[0].Data.ShouldBe(_screenshots[0].Data);
            screenshots[0].Url.ShouldBe(_screenshots[0].Url);
            screenshots[1].Data.ShouldBe(_screenshots[1].Data);
            screenshots[1].Url.ShouldBe(_screenshots[1].Url);
            screenshotResponse.Count.ShouldBe(_screenshots.Count);
        }

        private static Domain.Screenshot CreateScreenshot(byte[] data, string url)
        {
            return new Domain.Screenshot { Data = data, Url = url };
        }
    }
}
