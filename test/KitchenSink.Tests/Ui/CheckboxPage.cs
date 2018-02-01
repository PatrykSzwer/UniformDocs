using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class CheckboxPage : BasePage
    {
        public CheckboxPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/checkbox-license-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/checkbox-input']")]
        public IWebElement Checkbox { get; set; }

        public void ToggleCheckbox()
        {
            ClickOn(Checkbox);
        }
    }
}
