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

        [FindsBy(How = How.CssSelector, Using = "input[type='date']")]
        public IWebElement DateInput { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-year-input']")]
        public IWebElement YearInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-month-input']")]
        public IWebElement MonthInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/datepicker-day-input']")]
        public IWebElement DayInput { get; set; }

        public void SelectThroughUniDatePicker(string date)
        {
            var script = $"return arguments[0].assignedSlot.parentElement.shadowRoot.querySelector('vaadin-date-picker')";
            var vaadinPicker = (IWebElement)ExecuteScriptOnElement(DateInput, script);
            var vaadinTextFieldElement = GetShadowElementByQuerySelector(vaadinPicker, "vaadin-text-field");
            var pickerInput = GetShadowElementByQuerySelector(vaadinTextFieldElement, "input[autocomplete=\"off\"]");
            pickerInput.Clear();
            pickerInput.SendKeys(date);
            pickerInput.SendKeys(Keys.Enter);
            
        }
    }
}