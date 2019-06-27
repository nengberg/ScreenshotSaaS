using System;
using System.Threading;
using System.Threading.Tasks;

using NSubstitute;

using OpenQA.Selenium;

using Screenshot.Domain;
using Screenshot.Processor;

using Shouldly;

using Xunit;

using Screenshot = OpenQA.Selenium.Screenshot;

namespace Screenshot.Tests
{
    public class GenerateScreenshotMessageHandlerTests
    {
        private readonly GenerateScreenshotMessageHandler _sut;
        private readonly ISaveScreenshotCommand _saveScreenshotCommand;
        private readonly INavigation _navigation;
        private readonly GenerateScreenshotMessage _message;
        private readonly OpenQA.Selenium.Screenshot _screenshot;

        public GenerateScreenshotMessageHandlerTests()
        {
            var webDriverFactory = Substitute.For<IWebDriverFactory>();
            var webDriver = Substitute.For<IWebDriver, ITakesScreenshot>();
            _navigation = Substitute.For<INavigation>();
            webDriver.Navigate().Returns(_navigation);
            _screenshot = new OpenQA.Selenium.Screenshot("iVBORw0KGgoA");
            ((ITakesScreenshot)webDriver).GetScreenshot().Returns(_screenshot);
            webDriverFactory.Create().Returns(webDriver);
            _saveScreenshotCommand = Substitute.For<ISaveScreenshotCommand>();
            _message = new GenerateScreenshotMessage("http://abc.se");
            _sut = new GenerateScreenshotMessageHandler(webDriverFactory, _saveScreenshotCommand);
        }

        [Fact]
        public async Task Handle_CalledWithMessage_NavigatesToUrlFromMessage()
        {
            await _sut.Handle(_message);

            _navigation.Received(1).GoToUrl(Arg.Is(_message.Url));
        }

        [Fact]
        public async Task Handle_WebDriverGeneratesScreenshotFromNavigatedUrl_CommandWithByteArrayFromThatScreenshotIsCalled()
        {
            await _sut.Handle(_message);

            await _saveScreenshotCommand.Received(1)
                .Execute(
                    Arg.Is<Domain.Screenshot>(s => s.Data == _screenshot.AsByteArray),
                    Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_MessageNull_ThrowsArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _sut.Handle(null));

            exception.ShouldBeOfType<ArgumentNullException>();
        }
    }
}