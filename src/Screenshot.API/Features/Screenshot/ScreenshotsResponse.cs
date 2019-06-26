using System.Collections.Generic;

namespace Screenshot.API.Features.Screenshot
{
    public class ScreenshotsResponse
    {
        public ScreenshotsResponse()
        {
            Screenshots = new List<ScreenshotResponse>();
        }

        public IEnumerable<ScreenshotResponse> Screenshots { get; set; }
    }
}