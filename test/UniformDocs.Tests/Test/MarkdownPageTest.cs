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
            WaitUntil(x => _markdown.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
