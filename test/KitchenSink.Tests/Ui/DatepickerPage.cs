using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Ui
{
    public class DatepickerPage : BasePage
    {
        public DatepickerPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-date']")]
        public IWebElement DateInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'pika-lendar']//table//tbody//td[@class = 'is-selected']//button[@class = 'pika-button pika-day']")]
        public IWebElement SelectedDay { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-year-input']")]
        public IWebElement YearInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-month-input']")]
        public IWebElement MonthInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-day-input']")]
        public IWebElement DayInput { get; set; }

        public void SelectDate(string date)
        {
            DateInput.Clear();
            DateInput.SendKeys(date);
            DateInput.SendKeys(Keys.Enter);
        }
    }
}
