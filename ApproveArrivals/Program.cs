using iPlan;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApproveArrivals
{
    class Program
    {
        static string _iPlanUsername;
        static string _iPlanPassword;

        static string _mightyTextUsername;
        static string _mightyTextPassword;

        static void Main(string[] args)
        {
            ValidateArguments(args);

            var iPlanInvitationsPage = new iPlan.Pages.InvitationsPage();
            var iPlanLoginPage = new iPlan.Pages.LoginPage();
            var mightyTextLoginPage = new MightyText.Pages.LoginPage();
            var mightyTextPage = new MightyText.Pages.MightTextPage();

            var phonesAnswers = new Dictionary<string, int>();
            var errorAnswers = new Dictionary<string, string>();

            bool success = false;

            iPlanLoginPage.Login(_iPlanUsername, _iPlanPassword);
            var phones = iPlanInvitationsPage.GetPhones();



            mightyTextLoginPage.Login(_mightyTextUsername, _mightyTextPassword);
            mightyTextPage.SendTexts(phones);
            

            //while (!success)
            //{
            //    try
            //    {
            //        mightyTextLoginPage.Login(_mightyTextUsername, _mightyTextPassword);
            //        phonesAnswers = mightyTextPage.CollectAnswers(out errorAnswers);
            //        success = true;
            //    }
            //    catch
            //    {
            //        success = false;
            //    }
            //}

            //try
            //{
            //    iPlanLoginPage.Login(_iPlanUsername, _iPlanPassword);
            //    iPlanInvitationsPage.UpdateArrivals(phonesAnswers, false);
            //    //iPlanInvitationsPage.UpdateErrors(errorAnswers);
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    Framework.DriverManager.Driver.Quit();
            //}
        }

        private static void ValidateArguments(string[] args)
        {
            //if (!Debugger.IsAttached)
            //{
            //    _iPlanUsername = args[0];
            //    _iPlanPassword = args[1];
            //    _mightyTextUsername = args[2];
            //    _mightyTextPassword = args[3];
            //}
            //else
            {
                _iPlanUsername = "someone@example.com";
                _iPlanPassword = "pass";
                _mightyTextUsername = "someone@example.com";
                _mightyTextPassword = "pass";
            }
        }
    }
}
