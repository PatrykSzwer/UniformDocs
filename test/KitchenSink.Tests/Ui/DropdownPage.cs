﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Ui
{
    public class DropdownPage : BasePage
    {
        public DropdownPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/dropdown-pets-dropdown']")]
        public IWebElement PetsSelect { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/dropdown-pets-reaction']")]
        public IWebElement PetLikeLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/dropdown-objects-select']")]
        public IWebElement ProductSelect { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/dropdown-objects-select-label'] > p")]
        public IWebElement JuicySelectLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".kitchensink-juicyselect select")]
        public IWebElement JuicySelect { get; set; }

        public void SelectPet(string petName)
        {
            SelectElement select = new SelectElement(PetsSelect);
            select.SelectByText(petName);
        }

        public void SelectProduct(string productName)
        {
            SelectElement select = new SelectElement(ProductSelect);
            select.SelectByText(productName);
        }

        public void SelectJuicySelect(string juicyName)
        {
            SelectElement select = new SelectElement(JuicySelect);
            select.SelectByText(juicyName);
        }
    }
}
