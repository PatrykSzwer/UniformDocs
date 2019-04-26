using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private ProgressBarPage _progressBarPage;

        public void InitProgressBarTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _progressBarPage = _mainPage.GoToProgressBarPage();
        }

        [Test]
        public void ProgressBarPage_ProgressBar_DownloadFile()
        {
            InitProgressBarTest();
            WaitUntil(x => _progressBarPage.ProgressBarButton.Displayed);
            var percentage = _progressBarPage.ProgressBarPercentage.Text;
            StringAssert.AreEqualIgnoringCase("0%", percentage);
            _progressBarPage.ProgressBarButton.Click();
            WaitUntil(x => _progressBarPage.ProgressBarPercentage.Text == "100%");
        }
        [Test]
        public void ProgressBarPage_GitHubSourceURL()
        {
            InitProgressBarTest();
            WaitUntil(x => _progressBarPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
