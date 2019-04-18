using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.ChromeNoV0)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class RadiolistPageTest : BaseTest
    {
        private RadiolistPage _radiolistPage;
        private MainPage _mainPage;

        public RadiolistPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _mainPage = new MainPage(Driver).GoToMainPage();
            _radiolistPage = _mainPage.GoToRadiolistPage();
        }

        [Test]
        public void RadiolistPage_RegularButton()
        {
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
            WaitUntil(x => _radiolistPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
