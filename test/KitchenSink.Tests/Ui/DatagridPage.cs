using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Linq;

namespace KitchenSink.Tests.Ui
{
    public class DatagridPage : BasePage
    {
        public DatagridPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Add a pet']")]
        public IWebElement AddPetButton { get; set; }

        public bool CheckTableVisible()
        {
            return GetHotTableShadowRoot().FindElement(By.ClassName("htCore")).Displayed;
        }

        public int GetTableRowsCount()
        {
            return GetHotTableShadowRoot().FindElement(By.ClassName("htCore")).FindElements(By.XPath("tbody//tr")).Count;
        }

        public IWebElement GetHotTableShadowRoot()
        {
            return ExpandShadowRoot(Driver.FindElement(By.XPath("//hot-table")));
        }

        public IReadOnlyCollection<IWebElement> GetCellsByText(string searchText)
        {
            return GetHotTableShadowRoot().FindElement(By.ClassName("htCore")).FindElements(By.XPath($"tbody//tr//td[text() = '{searchText}']"));
        }

        public int GetCatsCount()
        {
            return GetCellsByText("Cat").Count;
        }

        public int GetMeowsCount()
        {
            return GetCellsByText("Meow").Count;
        }

        public void ReplaceTextInACell(string searchText, string newText)
        {
            var td = GetCellsByText(searchText).First();
            DblClickOn(td);
            var input = GetHotTableShadowRoot().FindElement(By.CssSelector("textarea.handsontableInput"));
            input.Clear();
            input.SendKeys(newText);
            input.SendKeys(Keys.Enter);
        }

        public void AddPet()
        {
            ClickOn(AddPetButton);
        }
    }
}
