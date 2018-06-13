using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class DatepickerPage : BasePage
    {
        public DatepickerPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }
        public IWebElement DateInput => Driver.FindElement(By.CssSelector("input[type='date']"));

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/datepicker-year-input']")]
        public IWebElement YearInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/datepicker-month-input']")]
        public IWebElement MonthInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/datepicker-day-input']")]
        public IWebElement DayInput { get; set; }

        public void SelectThroughUniDatePicker(string date)
        {
            var script = "return arguments[0].assignedSlot.parentElement.shadowRoot.querySelector('vaadin-date-picker')";
            var vaadinPicker = (IWebElement)ExecuteScriptOnElement(DateInput, script);
            var vaadinTextFieldElement = GetShadowElementByQuerySelector(vaadinPicker, "vaadin-text-field");
            var pickerInput = GetShadowElementByQuerySelector(vaadinTextFieldElement, "input[autocomplete=\"off\"]");
            pickerInput.Clear();
            pickerInput.SendKeys(date);
            pickerInput.SendKeys(Keys.Enter);
        }
    }
}