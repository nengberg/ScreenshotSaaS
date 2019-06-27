using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Screenshot.Domain;

namespace Screenshot.API.Features.Screenshot
{
    public class GetScreenshotsRequestHandler : IRequestHandler<GetScreenshotsRequest, ScreenshotsResponse>
    {
        private readonly IGetScreenshotsQuery _getScreenshotsQuery;

        public GetScreenshotsRequestHandler(IGetScreenshotsQuery getScreenshotsQuery)
        {
            _getScreenshotsQuery = getScreenshotsQuery;
        }

        public async Task<ScreenshotsResponse> Handle(GetScreenshotsRequest request, CancellationToken cancellationToken)
        {
            var response = new ScreenshotsResponse();
            var result = await _getScreenshotsQuery.Execute(cancellationToken);
            response.Count = result.Count();
            response.Screenshots = MapScreenshots(result);

            return response;
        }

        private static IEnumerable<ScreenshotResponse> MapScreenshots(IEnumerable<Domain.Screenshot> screenshots)
        {
            return screenshots.Select(screenshot => new ScreenshotResponse
            {
                Data = screenshot.Data
            });
        }
    }
}