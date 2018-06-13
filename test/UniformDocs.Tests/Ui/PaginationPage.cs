using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace UniformDocs.Tests.Ui
{
    public class PaginationPage : BasePage
    {
        public PaginationPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/pagination-juicy-select-items-per-page'] select")]
        public IWebElement DropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/pagination-table'] tbody > tr")]
        public IList<IWebElement> PaginationResult { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/pagination-navigation'] ul li")]
        public IWebElement Pagination { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/pagination-page-number']")]
        public IWebElement PaginationInfoLabel { get; set; }

        public void DropdownSelect(string option)
        {
            SelectElement dropDown = new SelectElement(DropDown);
            dropDown.SelectByText(option);
        }

        internal void GoToPage(string pageNumber)
        {
            ClickOn(Driver.FindElement(By.XPath($"//a[text() = '{pageNumber}']")));
        }
    }
}
