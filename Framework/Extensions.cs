using System;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace Framework
{
    public static class Extensions
    {
        public static bool WaitForElement(this IWebDriver driver, By by, int secondsToWait = 30)
        {
            var timeout = new TimeSpan(0, 0, secondsToWait);
            var sw = new Stopwatch();
            bool found = false;

            sw.Start();
            while (!found && sw.ElapsedMilliseconds < timeout.TotalMilliseconds)
            {
                var elements = driver.FindElements(by);
                if (elements.Any())
                {
                    found = true;
                }
                else
                {
                    Thread.Sleep(500);
                }
            }

            return found;
        }

        public static bool WaitForElementToDisappear(this IWebDriver driver, By by, int secondsToWait = 30)
        {
            var timeout = new TimeSpan(0, 0, secondsToWait);
            var sw = new Stopwatch();
            bool found = true;

            sw.Start();
            while (found && sw.ElapsedMilliseconds < timeout.TotalMilliseconds)
            {
                var elements = driver.FindElements(by);
                if (!elements.Any())
                {
                    found = false;
                }
                else
                {
                    Thread.Sleep(500);
                }
            }

            return found;
        }

        internal static bool IsJQueryComplete()
        {
            return (bool)((IJavaScriptExecutor)DriverManager.Driver).ExecuteScript("return jQuery.active == 0");
        }

        public static void WaitForAjax(this IWebDriver driver, int secondsToWait = 60)
        {
            var timeout = new TimeSpan(0, 0, secondsToWait);
            var sw = new Stopwatch();
            bool done = false;

            sw.Start();
            while (!done && sw.ElapsedMilliseconds < timeout.TotalMilliseconds)
            {
                done = IsJQueryComplete();

                if (!done)
                {
                    Thread.Sleep(500);
                }
            }
            if (!done)
            {
                throw new TimeoutException("WaitForAjax did not complete within " + secondsToWait + " seconds");
            }
        }

        public static void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)DriverManager.Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}
