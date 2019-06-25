using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Screenshot.Processor
{
    public class CaptureScreenshotMessageHandler
    {
        public Task Handle(CaptureScreenshotMessage message)
        {
            using(var driver = CreateFirefoxDriver())
            {
                driver.Navigate().GoToUrl(message.Url);
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                //screenshot.SaveAsFile($@"{Directory.GetCurrentDirectory()}\{Guid.NewGuid()}.png");
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
