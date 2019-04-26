using System.Drawing.Design;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;


namespace UniformDocs.Tests.Test
{
    partial class BaseTest
    {
        private CardPage _cardPage;

        public void InitCardPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _cardPage = _mainPage.GoToCardPage();
        }
        [Test]
        public void CardPage_GitHubSourceURL()
        {
            InitCardPageTest();
            WaitUntil(x => _cardPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
