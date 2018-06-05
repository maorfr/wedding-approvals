using Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace iPlan.Pages
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
            Password = password;
            Submit();
        }
    }
}
