using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class ProgressBarPage : BasePage
    {
        public ProgressBarPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/progressBar-button']")]
        public IWebElement ProgressBarButton { get; set; }

        [FindsBy(How = How.ClassName, Using = "uniformdocs-progress-bar__percentages")]
        public IWebElement ProgressBarPercentage { get; set; }
    }
}
