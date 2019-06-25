using Screenshot.Core.BuildingBlocks;

namespace Screenshot.Processor
{
    public class CaptureScreenshotMessage : IMessage
    {
        public CaptureScreenshotMessage(string url)
        {
            //Validate
            Url = url;
        }

        public string Url { get; }
    }
}
