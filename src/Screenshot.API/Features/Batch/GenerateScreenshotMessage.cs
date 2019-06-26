using Screenshot.Infrastructure;

namespace Screenshot.API.Features.Batch
{
    public class GenerateScreenshotMessage : IMessage
    {
        public GenerateScreenshotMessage(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}