using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class ToggleButtonPage : BasePage
    {
        public ToggleButtonPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/toggleButton-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/toggleButton-toggle-button']")]
        public IWebElement ToogleButton { get; set; }

        public void ChangeToggleButtonState()
        {
            ClickOn(ToogleButton);
        }
    }
}
