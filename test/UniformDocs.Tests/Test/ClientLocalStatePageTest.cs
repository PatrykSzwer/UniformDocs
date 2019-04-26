using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private ClientLocalStatePage _clientLocalStatePage;

        public void InitClientLocalStatePageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _clientLocalStatePage = _mainPage.GoToClientLocalStatePage();
        }

        [Test]
        public void ClientLocalStatePage_HoverListItemExpectHoveredTextToShow()
        {
            InitClientLocalStatePageTest();
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
            InitClientLocalStatePageTest();
            WaitUntil(x => _clientLocalStatePage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
