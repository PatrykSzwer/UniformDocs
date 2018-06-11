using System.Drawing.Design;
using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;


namespace KitchenSink.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class CardPageTest : BaseTest
    {
        private CardPage _cardPage;
        private MainPage _mainPage;

        public CardPageTest(Config.Browser browser) : base(browser)
        {
        }


        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _cardPage = _mainPage.GoToCardPage();
        }
        [Test]
        public void CardPage_GitHubSourceURL()
        {
            WaitUntil(x => _cardPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
