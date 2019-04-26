using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private RedirectPage _redirectPage;

        public void InitRedirectPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _redirectPage = _mainPage.GoToRedirectPage();
        }

        [Test]
        public void RedirectPage_ClickingOnFruitShouldChangeUrlAndText()
        {
            InitRedirectPageTest();
            _redirectPage.ClickButton(Config.Buttons.Fruit);
            Assert.IsTrue(WaitForText(_redirectPage.InfoLabel, "You've got some tasty apple", 20));
            Assert.AreEqual(Config.TestedAppUrl + "/Redirect/apple", Driver.Url);

            _redirectPage.ClickButton(Config.Buttons.Vegetable);
            Assert.IsTrue(WaitForText(_redirectPage.InfoLabel, "You've got some tasty carrot", 20));
            Assert.AreEqual(Config.TestedAppUrl + "/Redirect/carrot", Driver.Url);

            _redirectPage.ClickButton(Config.Buttons.Bread);
            Assert.IsTrue(WaitForText(_redirectPage.InfoLabel, "You've got some tasty baguette", 20));
            Assert.AreEqual(Config.TestedAppUrl + "/Redirect/baguette", Driver.Url);
        }

        [Test]
        public void RedirectPage_ClickingOnRedirectToAnotherPartialShouldChangeUrl()
        {
            InitRedirectPageTest();
            _redirectPage.ClickButton(Config.Buttons.Morph);
            WaitUntil(x => Driver.Url == Config.TestedAppUrl.ToString());
            Assert.AreEqual(Config.TestedAppUrl.ToString(), Driver.Url);
        }

        [Test]
        public void RedirectPage_ClickingOnRedirectToExternalWebsiteShouldChangeUrl()
        {
            InitRedirectPageTest();
            _redirectPage.ClickButton(Config.Buttons.Redirect);
            WaitUntil(x => Driver.Url == "https://starcounter.io/");
            Assert.AreEqual("https://starcounter.io/", Driver.Url);
        }
        [Test]
        public void RedirectPage_GitHubSourceURL()
        {
            InitRedirectPageTest();
            WaitUntil(x => _redirectPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
