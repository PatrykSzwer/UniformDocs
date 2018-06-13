using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class PasswordPage : BasePage
    {
        public PasswordPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/password-input']")]
        public IWebElement PasswordInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/password-label']")]
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