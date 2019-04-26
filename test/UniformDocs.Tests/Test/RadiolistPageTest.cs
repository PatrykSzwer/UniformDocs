using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private RadiolistPage _radiolistPage;

        public void InitRadiolistPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _radiolistPage = _mainPage.GoToRadiolistPage();
        }

        [Test]
        public void RadiolistPage_RegularButton()
        {
            InitRadiolistPageTest();
            WaitUntil(x => _radiolistPage.InfoLabel.Displayed);
            Assert.IsTrue(WaitForText(_radiolistPage.InfoLabel, "Dogs", 5));

            _radiolistPage.SelectRadio("Cats");
            Assert.IsTrue(WaitForText(_radiolistPage.InfoLabel, "Cats", 5));

            _radiolistPage.SelectRadio("Dogs");
            Assert.IsTrue(WaitForText(_radiolistPage.InfoLabel, "Dogs", 5));
        }
        [Test]
        public void RadiolistPage_GitHubSourceURL()
        {
            InitRadiolistPageTest();
            WaitUntil(x => _radiolistPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
