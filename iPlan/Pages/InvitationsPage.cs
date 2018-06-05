using Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using iPlan.Extensions;

namespace iPlan.Pages
{
    public partial class InvitationsPage
    {
        public void Navigate()
        {
            DriverManager.Driver.Navigate().GoToUrl(Url);
        }

        private IWebElement PageSizeField { get { return DriverManager.Driver.FindElement(By.Id("paging_page_size")); } }

        public void SetPageSize(int rowsToShow)
        {
            PageSizeField.Click();
            PageSizeField.SendKeys(rowsToShow.ToString() + Keys.Enter);
            DriverManager.Driver.WaitForElement(By.XPath("//select[@id = 'paging_page_size']/option[@value='1000' and @selected]"));
        }

        private string GetPhone(IWebElement row)
        {
            return _getPhone(row);
        }
        public List<string> GetPhones()
        {
            SetPageSize(1000);

            var phonesList = new List<string>();

            var rows = Rows;

            foreach (var row in Rows)
            {
                Framework.Extensions.ScrollToElement(row);
                var phoneNumber = GetPhone(row);

                if (phoneNumber.Equals(""))
                    continue;

                phonesList.Add(phoneNumber);
            }

            return phonesList;
        }

        public void UpdateErrors(Dictionary<string, string> errorAnswers)
        {
            var rows = Rows;

            foreach (var row in rows)
            {
                Framework.Extensions.ScrollToElement(row);
                var phoneNumber = GetPhone(row);

                if (phoneNumber.Equals(""))
                    continue;

                if (!errorAnswers.ContainsKey(phoneNumber))
                    continue;

                var clickToUpdate = row.FindElements(By.XPath(".//td[@class = 'header_medium editable qtip_editor']")).Last();
                clickToUpdate.Click();

                string message = errorAnswers[phoneNumber];
            }
        }

        public void UpdateArrivalInRow(IWebElement row, int answer)
        {
            _updateArrivalInRow(row, answer);
        }

        public void UpdateArrivals(Dictionary<string, int> phonesAnswers, bool shouldOverrideOnConflict)
        {
            SetPageSize(1000);

            var rows = Rows;

            foreach (var row in rows)
            {
                Framework.Extensions.ScrollToElement(row);
                var phoneNumber = GetPhone(row);

                if (phoneNumber.Equals(""))
                    continue;

                if (!phonesAnswers.ContainsKey(phoneNumber))
                    continue;

                int answerArrival = phonesAnswers[phoneNumber];

                var arrivals = row.GetArrivalElements();

                int arrivalNumber = -1;

                if (arrivals.Any())
                    arrivalNumber = int.Parse(arrivals.First().Text);

                if (arrivalNumber.Equals(answerArrival))
                {
                    continue;
                }
                else
                {
                    if (!shouldOverrideOnConflict)
                        continue;
                }

                UpdateArrivalInRow(row, answerArrival);
            }
        }
    }
}
