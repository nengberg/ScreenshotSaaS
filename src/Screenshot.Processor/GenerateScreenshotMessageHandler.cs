using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

using Screenshot.Infrastructure;

namespace Screenshot.Processor
{
    public class GenerateScreenshotMessageHandler : IMessageHandler<GenerateScreenshotMessage>
    {
        private readonly ISaveScreenshotCommand _saveScreenShotCommand;

        public GenerateScreenshotMessageHandler(ISaveScreenshotCommand saveScreenShotCommand)
        {
            _saveScreenShotCommand = saveScreenShotCommand;
        }

        public async Task Handle(GenerateScreenshotMessage message)
        {
            Console.WriteLine($"Generating screenshot from URL {message.Url}");
            using(var driver = CreateFirefoxDriver())
            {
                driver.Navigate().GoToUrl(message.Url);
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                await _saveScreenShotCommand.Execute(new Infrastructure.Screenshot { Data = screenshot.AsByteArray });
                //screenshot.SaveAsFile($@"{Directory.GetCurrentDirectory()}\{Guid.NewGuid()}.png");
            }
        }

        private static IWebDriver CreateFirefoxDriver()
        {
            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var service = FirefoxDriverService.CreateDefaultService(driverPath, "geckodriver.exe");
            var options = new FirefoxOptions();
            options.AddArguments("--headless");
            return new FirefoxDriver(service, options);
        }
    }
}
