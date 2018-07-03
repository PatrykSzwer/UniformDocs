using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class MarkdownPageTest : BaseTest
    {
        private MarkdownPage _markdown;
        private MainPage _mainPage;

        public MarkdownPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _markdown = _mainPage.GoToMarkdownPage();
        }

        [Test]
        public void MarkdownPage_CheckPreviewText()
        {
            WaitUntil(x => _markdown.MarkedElement.Displayed);

            Assert.AreEqual("This is a structured text", _markdown.GetHeaderText());
        }
        [Test]
        public void MarkdownPage_GitHubSourceURL()
        {
            WaitUntil(x => _markdown.GitHubSourceLinks.Displayed, "", 10, 3);
            TestGitHubSourceLinkURLs();
        }
    }
}
