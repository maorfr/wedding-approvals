using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Framework
{
    public class DriverManager
    {
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    var co = new ChromeOptions();
                    co.AddArguments("--start-maximized", "--disable-extensions");
                    _driver = new ChromeDriver(@"C:\ChromeDriver", co);
                }

                return _driver;
            }
        }
        private static IWebDriver _driver;

        public static void Stop()
        {
            Driver.Quit();
        }
    }
}
