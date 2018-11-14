using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [TestFixture(Config.Browser.iOS11point2)]
    [TestFixture(Config.Browser.Android7Nougat)]
    [TestFixture(Config.Browser.Android8Oreo)]
    [TestFixture(Config.Browser.iOS11point4)]
    [TestFixture(Config.Browser.iOS12)]
    [TestFixture(Config.Browser.iOS11point3)]
    [TestFixture(Config.Browser.Android7Nougat)]
    [TestFixture(Config.Browser.Android6Marshmello)]
    [TestFixture(Config.Browser.Android8point1Oreo)]
    [TestFixture(Config.Browser.Android7point1Nougat)]
    [TestFixture(Config.Browser.Android5point1Lolliopop)]
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
