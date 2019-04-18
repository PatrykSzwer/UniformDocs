using System.Drawing.Design;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;


namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.ChromeNoV0)]
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
        public override void SetUp()
        {
            base.SetUp();

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
