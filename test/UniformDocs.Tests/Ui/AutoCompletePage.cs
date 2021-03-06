﻿using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class AutoCompletePage : BasePage
    {
        public AutoCompletePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/autocomplete-products-input']")]
        public IWebElement ProductsInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/autocomplete-places-input']")]
        public IWebElement PlaceInput { get; set; }
        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/autocomplete-products-autocomplete'] li")]
        public IList<IWebElement> ProductsAutoComplete { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/autocomplete-places-autocomplete'] li")]
        public IList<IWebElement> PlacesAutoComplete { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/autocomplete-places-capital']")]
        public IWebElement PlaceInfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/autocomplete-products-price']")]
        public IWebElement ProductsInfoLabel { get; set; }

        public void ChoosePlace(string place)
        {
            ClickOn(PlacesAutoComplete.First(x => x.Text == place));
        }

        public void ChooseProducts(string product)
        {
            ClickOn(ProductsAutoComplete.First(x => x.Text == product));
        }
    }
}
