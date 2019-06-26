using MediatR;

namespace Screenshot.API.Features.Screenshot
{
    public class GetScreenshotsRequest : IRequest<ScreenshotsResponse> { }
}