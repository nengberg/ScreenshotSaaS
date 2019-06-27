using OpenQA.Selenium;

namespace Screenshot.Processor
{
    public interface IWebDriverFactory
    {
        IWebDriver Create();
    }
}