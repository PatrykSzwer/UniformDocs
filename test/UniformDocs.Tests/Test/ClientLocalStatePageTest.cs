using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    //[TestFixture(Config.Browser.ChromeNoV0)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [Parallelizable(ParallelScope.Fixtures)]
    class ClientLocalStateTest : BaseTest
    {
        private ClientLocalStatePage _clientLocalStatePage;
        private MainPage _mainPage;

        public ClientLocalStateTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _clientLocalStatePage = _mainPage.GoToClientLocalStatePage();
        }

        [Test]
        public void ClientLocalStatePage_HoverListItemExpectHoveredTextToShow()
        {
            WaitUntil(x => _clientLocalStatePage.HoverableList.Displayed);

            // Hover the first item
            WaitUntil(x => _clientLocalStatePage.GetFirstHoverObservor().Displayed);
            _clientLocalStatePage.HoverFirstElement();
            WaitUntil(x => _clientLocalStatePage.GetFirstHoverObservor().Text.Contains("Hovered"));
            // hovering an item shouldn't affect the other
            Assert.False(_clientLocalStatePage.GetSecondHoverObservor().Text.Contains("Hovered"));

            System.Threading.Thread.Sleep(1000); //wait for mouse leave

            // Hover the second item
            WaitUntil(x => _clientLocalStatePage.GetSecondHoverObservor().Displayed);
            _clientLocalStatePage.HoverSecondElement();
            WaitUntil(x => _clientLocalStatePage.GetSecondHoverObservor().Text.Contains("Hovered"));
            // hovering an item shouldn't affect the other
            Assert.False(_clientLocalStatePage.GetFirstHoverObservor().Text.Contains("Hovered"));

        }
        [Test]
        public void ClientLocalStatePage_GitHubSourceURL()
        {
            WaitUntil(x => _clientLocalStatePage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
