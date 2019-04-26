using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private MarkdownPage _markdown;

        public void InitMarkdownPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _markdown = _mainPage.GoToMarkdownPage();
        }

        [Test]
        public void MarkdownPage_CheckPreviewText()
        {
            InitMarkdownPageTest();
            WaitUntil(x => _markdown.MarkedElement.Displayed);

            Assert.AreEqual("This is a structured text", _markdown.GetHeaderText());
        }
        [Test]
        public void MarkdownPage_GitHubSourceURL()
        {
            InitMarkdownPageTest();
            WaitUntil(x => _markdown.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
