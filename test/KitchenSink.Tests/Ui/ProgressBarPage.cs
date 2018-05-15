using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class ProgressBarPage : BasePage
    {
        public ProgressBarPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/progressBar-button']")]
        public IWebElement ProgressBarButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "kitchensink-progress-bar__percentages")]
        public IWebElement ProgressBarPercentage { get; set; }
    }
}
