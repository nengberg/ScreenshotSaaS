namespace Screenshot.Processor
{
    public class CaptureScreenshotMessage
    {
        public CaptureScreenshotMessage(string url)
        {
            //Validate
            Url = url;
        }

        public string Url { get; }
    }
}
