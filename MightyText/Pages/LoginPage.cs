using OpenQA.Selenium;
using Framework;
using System.Linq;
using System.Collections.Generic;

namespace MightyText.Pages
{
    public partial class LoginPage
    {
        public void Navigate()
        {
            DriverManager.Driver.Navigate().GoToUrl(_url);
        }

        public void Login(string username, string password)
        {
            Navigate();
            Username = username;
            Next();
            Password = password;
            Next();

            //ApproveAlert();
            //EndTour();

            DriverManager.Driver.WaitForElement(By.XPath("//span[text() = 'New Message']"));
        }
    }
}
