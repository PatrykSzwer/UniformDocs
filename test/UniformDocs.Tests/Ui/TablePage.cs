using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class TablePage : BasePage
    {
        public TablePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Add a pet']")]
        public IWebElement AddPetButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/table-table']")]
        public IWebElement PetsTable { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/table-table'] tbody tr")]
        public IList<IWebElement> PetsTableRows { get; set; }

        public void AddPet()
        {
            ClickOn(AddPetButton);
        }
    }
}
