using OpenQA.Selenium;
using Framework;
using System.Linq;
using System.Collections.Generic;

namespace MightyText.Pages
{
    public class MightTextPage
    {
        private IWebElement NewMessageContainer { get { return DriverManager.Driver.FindElement(By.XPath("//div[@class = 'composeNewMessageWrapper']")); } }

        private IWebElement NewMessageButton { get { return DriverManager.Driver.FindElement(By.XPath("//span[text() = 'New Message']")); } }

        private void OpenNewMessage()
        {
            bool found = false;

            while (!found)
            {
                NewMessageButton.Click();
                found = DriverManager.Driver.WaitForElement(By.Id("selectContactForSingleCompose"), 5);
            }
        }

        private IWebElement PhoneInput { get { return NewMessageContainer.FindElement(By.Id("selectContactForSingleCompose")); } }

        private IWebElement MessageInput { get { return NewMessageContainer.FindElement(By.Id("send-one-text")); } }

        private IWebElement SendButton { get { return NewMessageContainer.FindElement(By.XPath(".//div[@class = 'dcsendicon']")); } }

        private string Message = "תודה שבאתם לחגוג איתנו בחתונה שלנו. נהנינו מאוד ומקווים שגם אתם! וכמובן תודה רבה על המתנה הנדיבה! שלי ומאור";

        public void SendTexts(List<string> phones)
        {
            foreach (var phone in phones)
            {
                OpenNewMessage();
                PhoneInput.SendKeys(phone + Keys.Enter);

                MessageInput.SendKeys(Message);
                SendButton.Click();
                DriverManager.Driver.WaitForElementToDisappear(By.XPath("//div[@class = 'composeNewMessageWrapper']"));
            }
        }

        private IWebElement SideTab { get { return DriverManager.Driver.FindElement(By.XPath("//ul[@id = 'navBarTabs']")); } }

        private IEnumerable<IWebElement> Rows { get { return SideTab.FindElements(By.XPath("./li")); } }

        public Dictionary<string, int> CollectAnswers(out Dictionary<string, string> _errorAnswers)
        {
            var phonesAnswers = new Dictionary<string, int>();
            var errorAnswers = new Dictionary<string, string>();

            var rows = Rows;
            foreach (var row in rows)
            {
                Framework.Extensions.ScrollToElement(row);
                var rowName = row.FindElement(By.XPath(".//span[@class = 'threadNameOrNumber']")).Text;
                row.Click();

                DriverManager.Driver.WaitForElement(By.XPath(string.Format("//div[@class = 'contentPanelHeaderText contentPanelHeaderTextNonGroup' and contains(text(), '{0}')]", rowName)));
                DriverManager.Driver.WaitForElement(By.XPath("//div[contains(@class, 'threadItem sentText')]"));

                string answerText = "";
                int successCode = 0; // 0 is false, 1 is true, 2 is no messages (need to continue to next row)

                while (successCode == 0)
                {
                    try
                    {
                        var answerMessages = DriverManager.Driver.FindElements(By.XPath("//div[@class = 'threadItem receivedText ']"));

                        if (!answerMessages.Any())
                        {
                            successCode = 2;
                            continue;
                        }

                        var answerMessage = answerMessages.Last();
                        answerText = answerMessage.FindElement(By.XPath(".//span[@class = 'message-body']")).Text;
                        successCode = 1;
                    }
                    catch (StaleElementReferenceException)
                    {
                        System.Threading.Thread.Sleep(500);
                        successCode = 0;
                    }
                }

                if (successCode == 2)
                {
                    continue;
                }

                int answerNumber;
                bool success = int.TryParse(answerText, out answerNumber);

                if (success)
                    phonesAnswers.Add(rowName.Replace("+972", "0"), answerNumber);
                else
                    errorAnswers.Add(rowName, answerText);
            }

            _errorAnswers = errorAnswers;
            return phonesAnswers;
        }
    }
}
