using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Screenshot.Processor.BuildingBlocks;

namespace Screenshot.Processor
{
    public class CaptureScreenshotCommandHandler : ICommandHandler<CaptureScreenshotCommand>
    {
        public Task Handle(CaptureScreenshotCommand command)
        {
            using(var driver = CreateFirefoxDriver())
            {
                driver.Navigate().GoToUrl(command.Url);
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                screenshot.SaveAsFile($@"{Directory.GetCurrentDirectory()}\{Guid.NewGuid()}.png");
            }
            return Task.CompletedTask;
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
