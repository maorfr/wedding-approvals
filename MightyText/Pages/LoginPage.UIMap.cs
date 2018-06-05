using OpenQA.Selenium;
using Framework;
using System.Linq;
using System.Collections.Generic;
using MightyText.Extensions;

namespace MightyText.Pages
{
    public partial class LoginPage
    {
        private string _url { get { return "https://mightytext.net/web8/"; } }

        private IWebElement UsernameField { get { return DriverManager.Driver.WaitAndFindElement(By.XPath("//input[@type = 'email']")); } }

        private string Username
        {
            set { UsernameField.SendKeys(value); }
        }

        private IWebElement PasswordField { get { return DriverManager.Driver.WaitAndFindElement(By.XPath("//input[@type = 'password']"), 1); } }

        private string Password
        {
            set { PasswordField.SendKeys(value); }
        }

        private IWebElement NextButton { get { return DriverManager.Driver.FindElement(By.XPath("//span[text() = 'Next']"));} }

        private void Next()
        {
            NextButton.Click();
        }

        private IWebElement CustomAlert { get { return DriverManager.Driver.WaitAndFindElement(By.Id("custom-alert-and-confirm-modal-ok-button")); } }

        private void ApproveAlert()
        {
            CustomAlert.Click();
        }

        private IWebElement EndTourButton { get { return DriverManager.Driver.WaitAndFindElement(By.XPath("//a[@class = 'end-tour sub-navigation']")); } }

        private void EndTour()
        {
            EndTourButton.Click();
        }
    }
}
