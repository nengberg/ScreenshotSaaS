using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

using Screenshot.Domain;
using Screenshot.Infrastructure;

namespace Screenshot.Processor
{
    public class GenerateScreenshotMessageHandler : IMessageHandler<GenerateScreenshotMessage>
    {
        private readonly IWebDriverFactory _webDriverFactory;
        private readonly ISaveScreenshotCommand _saveScreenshotCommand;

        public GenerateScreenshotMessageHandler(IWebDriverFactory webDriverFactory, ISaveScreenshotCommand saveScreenshotCommand)
        {
            _webDriverFactory = webDriverFactory;
            _saveScreenshotCommand = saveScreenshotCommand;
        }

        public async Task Handle(GenerateScreenshotMessage message)
        {
            if(message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Console.WriteLine($"Generating screenshot from URL {message.Url}");

            using(var driver = _webDriverFactory.Create())
            {
                driver.Navigate().GoToUrl(message.Url);
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                await _saveScreenshotCommand.Execute(CreateScreenshot(message, screenshot.AsByteArray), CancellationToken.None);
            }
        }

        private static Domain.Screenshot CreateScreenshot(GenerateScreenshotMessage message, byte[] data)
        {
            return new Domain.Screenshot
            {
                Url = message.Url,
                Data = data
            };
        }
    }
}
