using Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Framework.Regionals;
using iPlan.Extensions;
using System.Threading;

namespace iPlan.Pages
{
    public partial class InvitationsPage
    {
        private string Url { get { return @"https://iplan.co.il/he-IL/client/events/64260/invitations"; } }

        private IEnumerable<IWebElement> Rows { get { return DriverManager.Driver.FindElements(By.XPath("//tr[@class = 'grid_row ']")); } }

        private string _getPhone(IWebElement row)
        {
            var phones = row.GetPhoneElements();

            string phoneNumber = "";

            if (!phones.Any())
                return phoneNumber;

            foreach (var phone in phones)
            {
                if (phone.Text.Equals(""))
                    continue;

                phoneNumber = phone.Text.NormalizePhoneText();
                if (!phoneNumber.IsValid(Regions.IL))
                    phoneNumber = "";
            }

            return phoneNumber;
        }

        private void _prepareToUpdate(IWebElement row)
        {
            var cellToUpdate = row.FindElements(By.XPath(".//td[@class = 'header_medium editable qtip_editor']")).Last();
            cellToUpdate.Click();
            DriverManager.Driver.WaitForElement(By.XPath("//label[@for = 'invitation_rsvp_status_not_arriving']"));
        }

        private IWebElement _notArrivingButton { get { return DriverManager.Driver.FindElement(By.XPath("//label[@for = 'invitation_rsvp_status_not_arriving']")); } }

        private void _setNotArriving()
        {
            _notArrivingButton.Click();
            Thread.Sleep(1000);
        }

        private IWebElement _arrivingButton { get { return DriverManager.Driver.FindElement(By.XPath("//label[@for = 'invitation_rsvp_status_arriving']")); } }

        private IWebElement _numberOfArrivalsDropDown { get { return DriverManager.Driver.FindElement(By.Id("invitation_rsvp_arriving_guests_count")); } }

        private int _numberOfArrivals
        {
            set { _numberOfArrivalsDropDown.SelectOption(value); }
        }
        private void _setArriving(int answer)
        {
            _arrivingButton.ClickAndWaitForElement(By.Id("invitation_rsvp_arriving_guests_count"));
            _numberOfArrivals = answer;
        }

        private IWebElement _submitArrivalsButton { get { return DriverManager.Driver.FindElements(By.Id("invitation_submit")).Last(); } }

        private void _submitArrivals()
        {
            _submitArrivalsButton.Click();
            DriverManager.Driver.WaitForAjax();
        }

        private void _performUpdate(int answer)
        {
            if (answer.Equals(0))
            {
                _setNotArriving();
            }
            else
            {
                _setArriving(answer);
            }

            _submitArrivals();
        }

        private void _updateArrivalInRow(IWebElement row, int answer)
        {
            _prepareToUpdate(row);
            _performUpdate(answer);
        }
    }
}
