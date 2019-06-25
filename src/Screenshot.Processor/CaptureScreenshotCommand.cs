using Screenshot.Processor.BuildingBlocks;

namespace Screenshot.Processor
{
    public class CaptureScreenshotCommand : ICommand
    {
        public CaptureScreenshotCommand(string url)
        {
            //Validate
            Url = url;
        }

        public string Url { get; }
    }
}
