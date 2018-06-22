using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Linq;

namespace UniformDocs.Tests.Ui
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
            bool isHotTableVisible = GetHtCore().Displayed;

            return isHotTableVisible;
        }

        public long GetTableRowsCount()
        {
            var tableRowsCount = GetHtCore().FindElements(By.XPath("tbody//tr")).Count;

            return tableRowsCount;
        }

        public IReadOnlyCollection<IWebElement> GetCellsByText(string searchText)
        {
            var cells = GetHtCore().FindElements(By.XPath($"tbody//tr//td[text() = '{searchText}']"));
            return cells;
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
            var input = GetShadowElementByQuerySelector(By.XPath("//hot-table"), "textarea.handsontableInput");
            input.Clear();
            input.SendKeys(newText);
            input.SendKeys(Keys.Enter);
        }

        public void AddPet()
        {
            ClickOn(AddPetButton);
        }

        private IWebElement GetHtCore()
        {
            return GetShadowElementByQuerySelector(By.XPath("//hot-table"), ".htCore");
        }
    }
}
