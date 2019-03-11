using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.ChromeNoV0)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class ProgressBarTest : BaseTest
    {
        private ProgressBarPage _progressBarPage;
        private MainPage _mainPage;

        public ProgressBarTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            if (!(RestApiHelper.CheckAppRunning(_testedAppName).Result))
            {
                Assert.Inconclusive($"The tested app {_testedAppName} is not running");
            }

            _mainPage = new MainPage(Driver).GoToMainPage();
            _progressBarPage = _mainPage.GoToProgressBarPage();
        }

        [Test]
        public void ProgressBarPage_ProgressBar_DownloadFile()
        {
            WaitUntil(x => _progressBarPage.ProgressBarButton.Displayed);
            var percentage = _progressBarPage.ProgressBarPercentage.Text;
            StringAssert.AreEqualIgnoringCase("0%", percentage);
            _progressBarPage.ProgressBarButton.Click();
            WaitUntil(x => _progressBarPage.ProgressBarPercentage.Text == "100%");
        }
        [Test]
        public void ProgressBarPage_GitHubSourceURL()
        {
            Assert.Inconclusive("Invocation of Assert.Inconclusive for testing the affect on TC build.");

            WaitUntil(x => _progressBarPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
