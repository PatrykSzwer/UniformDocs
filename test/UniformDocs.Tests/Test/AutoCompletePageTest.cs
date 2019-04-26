using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest
    {
        private AutoCompletePage _autoCompletePage;

        public void InitAutoCompletePageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _autoCompletePage = _mainPage.GoToAutoCompletePage();
        }

        [Test]
        public void AutoCompletePage_FillStarExpectAllItemsShowUp()
        {
            InitAutoCompletePageTest();
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
            InitAutoCompletePageTest();
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
            InitAutoCompletePageTest();
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
            InitAutoCompletePageTest();
            WaitUntil(x => _autoCompletePage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
