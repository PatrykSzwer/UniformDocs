using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Ui
{
    public class PaginationPage : BasePage
    {
        public PaginationPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-juicyselect select")]
        public IWebElement DropDown { get; set; }

        [FindsBy(How = How.XPath, Using = "//table[@slot = 'kitchensink/pagination-table']/tbody/descendant::tr")]
        public IList<IWebElement> PaginationResult { get; set; }

        [FindsBy(How = How.XPath, Using = "//nav[@slot = 'kitchensink/pagination-navigation']/ul/li")]
        public IWebElement Pagination { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[@slot = 'kitchensink/pagination-page-number']")]
        public IWebElement PaginationInfoLabel { get; set; }

        public void DropdownSelect(string option)
        {
            SelectElement dropDown = new SelectElement(DropDown);
            dropDown.SelectByText(option);
        }

        internal void GoToPage(string pageNumber)
        {
            ClickOn(Pagination.FindElement(By.XPath("//span[text() = '" + pageNumber + "']")));
        }
    }
}
