using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class CardPage : BasePage
    {
        public CardPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }
    }
}
