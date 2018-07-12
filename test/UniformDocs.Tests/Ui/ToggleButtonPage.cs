using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class ToggleButtonPage : BasePage
    {
        public ToggleButtonPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/toggleButton-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/toggleButton-toggle-button']")]
        public IWebElement ToogleButton { get; set; }

        public void ChangeToggleButtonState()
        {
            ClickOn(ToogleButton);
        }
    }
}
