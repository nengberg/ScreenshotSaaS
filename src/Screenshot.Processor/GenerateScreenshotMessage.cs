using Screenshot.Infrastructure;

namespace Screenshot.Processor
{
    public class GenerateScreenshotMessage : IMessage
    {
        public GenerateScreenshotMessage(string url)
        {
            //Validate
            Url = url;
        }

        public string Url { get; }
    }
}
