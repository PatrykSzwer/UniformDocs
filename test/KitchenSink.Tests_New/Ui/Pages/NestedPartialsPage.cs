﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;

namespace KitchenSink.Test
{
    public class NestedPartialsPage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//button[text() = 'Add child']")]
        public IWebElement addChildButton { get; set; }

        [FindsByAll]
        [FindsBy(How = How.XPath, Using = "//div[@class = 'kitchensink-nested-child']")]
        public IList<IWebElement> childDivs { get; set; }
        
        public NestedPartialsPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void AddChild()
        {
            ClickOn(addChildButton);
        }

        public int CountChildDivs()
        {
            return childDivs.Count;
        }
    }
}
