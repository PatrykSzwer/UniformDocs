﻿using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class RadiolistPage : BasePage
    {
        public RadiolistPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[@slot = 'kitchensink/radiolist-selected-item-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//paper-listbox[@slot = 'kitchensink/radiolist-paper-listbox']/descendant::paper-item")]
        public IList<IWebElement> Radios { get; set; }


        public void SelectRadio(string radioName)
        {
            ClickOn(Radios.Single(x => x.Text == radioName));
        }
    }
}
