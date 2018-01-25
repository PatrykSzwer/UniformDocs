﻿using System.Collections.Generic;
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

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-juicy-select select")]
        public IWebElement DropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-pagination-result-table-row")]
        public IList<IWebElement> PaginationResult { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-test-pagination-label")]
        public IWebElement PaginationInfoLabel { get; set; }

        public void DropdownSelect(string option)
        {
            SelectElement dropDown = new SelectElement(DropDown);
            dropDown.SelectByText(option);
        }

        internal void GoToPage(string pageNumber)
        {
            ClickOn(Driver.FindElement(By.XPath($"//span[text() = '{pageNumber}']")));
        }
    }
}
