using System;
using System.IO;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Screenshot.Processor
{
    public class WebDriverFactory : IWebDriverFactory
    {
        public IWebDriver Create()
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