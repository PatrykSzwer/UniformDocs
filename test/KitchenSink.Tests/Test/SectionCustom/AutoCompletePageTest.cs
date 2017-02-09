﻿using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Ui.SectionCustom;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Test.SectionCustom
{
    [TestFixture(Config.Browser.Chrome, "Running AutoComplete Page Test on Chrome")]
    [TestFixture(Config.Browser.Edge, "Running AutoComplete Page Test on Edge")]
    [TestFixture(Config.Browser.Firefox, "Running AutoComplete Page Test on Firefox")]
    class AutoCompletePageTest : BaseTest
    {
        private AutoCompletePage _autoCompletePage;
        private MainPage _mainPage;

        public AutoCompletePageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _autoCompletePage = _mainPage.GoToAutoCompletePage();
        }

        [Test]
        public void AutoCompletePage_FillStarExpectAllItemsShowUp()
        {
            WaitUntil(x => _autoCompletePage.ProductsInput.Displayed);
            _autoCompletePage.ProductsInput.Clear();
            _autoCompletePage.ProductsInput.SendKeys("*");
            WaitUntil(x => _autoCompletePage.ProductsAutoComplete.Count > 0);
            Assert.AreEqual(6, _autoCompletePage.ProductsAutoComplete.Count);

            WaitUntil(x => _autoCompletePage.PlaceInput.Displayed);
            _autoCompletePage.PlaceInput.Clear();
            _autoCompletePage.PlaceInput.SendKeys("*");
            WaitUntil(x => _autoCompletePage.PlacesAutoComplete.Count > 0);
            Assert.AreEqual(7, _autoCompletePage.PlacesAutoComplete.Count);
        }

        [Test]
        public void AutoCompletePage_FillCountryNameThenSelectCountry()
        {
            WaitUntil(x => _autoCompletePage.PlaceInput.Displayed);
            _autoCompletePage.PlaceInput.Clear();
            _autoCompletePage.PlaceInput.SendKeys("P");
            WaitUntil(x => _autoCompletePage.PlacesAutoComplete.Count > 0);
            _autoCompletePage.ChoosePlace("Poland");
            Assert.IsTrue(WaitUntil(ExpectedConditions.TextToBePresentInElement(_autoCompletePage.PlaceInfoLabel, "Capital of Poland is Warsaw")));
        }

        [Test]
        public void AutoCompletePage_FillProductNameThenSelectProduct()
        {
            WaitUntil(x => _autoCompletePage.ProductsInput.Displayed);
            _autoCompletePage.PlaceInput.Clear();
            _autoCompletePage.ProductsInput.SendKeys("B");
            WaitUntil(x => _autoCompletePage.ProductsAutoComplete.Count > 0);
            _autoCompletePage.ChooseProducts("Bread");
            Assert.IsTrue(WaitUntil(ExpectedConditions.TextToBePresentInElement(_autoCompletePage.ProductsInfoLabel, "Bread costs $1")));
        }
    }
}
