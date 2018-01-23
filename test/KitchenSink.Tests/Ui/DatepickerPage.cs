using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class DatepickerPage : BasePage
    {
        public DatepickerPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.TagName, Using = "vaadin-date-picker")]
        public IWebElement DatePicker { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'pika-lendar']//table//tbody//td[@class = 'is-selected']//button[@class = 'pika-button pika-day']")]
        public IWebElement SelectedDay { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-year-input")]
        public IWebElement YearInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-month-input")]
        public IWebElement MonthInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-day-input")]
        public IWebElement DayInput { get; set; }

        public void SelectDate(string date)
        {
            DatePicker = Driver.FindElement(By.TagName("vaadin-date-picker")); // Has to be found again due to incorrect type - EventFiringWebElement of current DatePicker object
            var vaadinTextFieldElement = GetShadowElementByQuerySelector(DatePicker, "vaadin-text-field");
            var pickerInput = GetShadowElementByQuerySelector(vaadinTextFieldElement, "input[autocomplete=\"off\"]");
            pickerInput.Clear();
            pickerInput.SendKeys(date);
            pickerInput.SendKeys(Keys.Enter);
        }
    }
}