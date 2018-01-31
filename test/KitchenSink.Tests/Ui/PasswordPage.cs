using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class PasswordPage : BasePage
    {
        public PasswordPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@slot = 'kitchensink/password-input']")]
        public IWebElement PasswordInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[@slot = 'kitchensink/password-label']")]
        public IWebElement PaswordInputInfoLabel { get; set; }

        public void FillPassword(string password)
        {
            PasswordInput.SendKeys(password);
        }

        public void ClearPassword()
        {
            PasswordInput.Clear();
        }
    }
}