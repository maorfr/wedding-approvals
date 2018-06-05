using OpenQA.Selenium;
using Framework;
using System.Collections;
using System.Collections.ObjectModel;

namespace iPlan.Extensions
{
    internal static class WebElementExtensions
    {
        internal static void SelectOption(this IWebElement dropDown, int option)
        {
            dropDown.Click();
            var element = dropDown.FindElement(By.XPath(string.Format("./option[@value = '{0}']", option)));
            element.Click();
        }

        internal static void ClickAndWaitForElement(this IWebElement element, By waitBy)
        {
            element.Click();
            DriverManager.Driver.WaitForElement(waitBy);
        }

        internal static ReadOnlyCollection<IWebElement> GetPhoneElements(this IWebElement row)
        {
            return row.FindElements(By.XPath(".//span[@class = 'phone ltr']"));
        }

        internal static ReadOnlyCollection<IWebElement> GetArrivalElements(this IWebElement row)
        {
            return row.FindElements(By.XPath(".//span[contains(@class, 'count') and contains(@class, 'vmiddle')]"));
        }
    }
}
