using OpenQA.Selenium;
using Framework;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace MightyText.Extensions
{
    internal static class WebElementExtensions
    {
        internal static IWebElement WaitAndFindElement(this IWebDriver driver, By by, int additionalWaitSeconds = 0, int secondsToWait = 30)
        {
            DriverManager.Driver.WaitForElement(by, secondsToWait);
            Thread.Sleep(additionalWaitSeconds * 1000);
            return DriverManager.Driver.FindElement(by);
        }
    }
}
