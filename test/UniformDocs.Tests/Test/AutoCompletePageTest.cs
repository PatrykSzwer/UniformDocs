﻿using System;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.ChromeNoV0)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class AutoCompletePageTest : BaseTest
    {
        private AutoCompletePage _autoCompletePage;
        private MainPage _mainPage;

        public AutoCompletePageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

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
            Assert.IsTrue(WaitForText(_autoCompletePage.PlaceInfoLabel, "Capital of Poland is Warsaw", 20));
        }

        [Test]
        public void AutoCompletePage_FillProductNameThenSelectProduct()
        {
            WaitUntil(x => _autoCompletePage.ProductsInput.Displayed);
            _autoCompletePage.PlaceInput.Clear();
            _autoCompletePage.ProductsInput.SendKeys("B");
            WaitUntil(x => _autoCompletePage.ProductsAutoComplete.Count > 0);
            _autoCompletePage.ChooseProducts("Bread");
            Assert.IsTrue(WaitForText(_autoCompletePage.ProductsInfoLabel, "Bread costs $1", 20));
        }
        [Test]
        public void AutoCompletePage_GitHubSourceURL()
        {
            WaitUntil(x => _autoCompletePage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
