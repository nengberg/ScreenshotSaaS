using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

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

            using(var driver = CreateWebDriver())
            {
                driver.Navigate().GoToUrl(message.Url);
                var screenshot = (driver as ITakesScreenshot).GetScreenshot();
                await _saveScreenShotCommand.Execute(new Infrastructure.Screenshot { Data = screenshot.AsByteArray });
            }

        }

        private static IWebDriver CreateWebDriver()
        {
            var driver = GetWebDriverType();
            if(driver == "Remote")
            {
                return new RemoteWebDriver(new Uri("http://seleniumhub:4444/wd/hub"), new FirefoxOptions());
            }

            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var service = FirefoxDriverService.CreateDefaultService(driverPath, "geckodriver.exe");
            var options = new FirefoxOptions();
            options.AddArguments("--headless");
            return new FirefoxDriver(service, options);
        }

        private static string GetWebDriverType()
        {
            var value = Environment.GetEnvironmentVariable("WebDriverType", EnvironmentVariableTarget.Machine);
            if(string.IsNullOrEmpty(value))
            {
                value = Environment.GetEnvironmentVariable("WebDriverType", EnvironmentVariableTarget.Process);
            }

            return value;
        }
    }
}
