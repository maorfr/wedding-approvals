using Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Framework.Regionals;

namespace iPlan.Pages
{
    public partial class LoginPage
    {
        private string _url { get { return @"https://iplan.co.il/he-IL/client/events/64260/invitations"; } }

        private IWebElement UsernameField { get { return DriverManager.Driver.FindElement(By.Id("client_account_email")); } }

        private string Username
        {
            get { return UsernameField.Text; }
            set { UsernameField.SendKeys(value); }
        }

        private IWebElement PasswordField { get { return DriverManager.Driver.FindElement(By.Id("client_account_password")); } }

        private string Password
        {
            get { return PasswordField.Text; }
            set { PasswordField.SendKeys(value); }
        }

        private IWebElement SubmitButton { get { return DriverManager.Driver.FindElement(By.Id("client_account_submit")); } }

        private void Submit()
        {
            SubmitButton.Click();
        }
    }
}
