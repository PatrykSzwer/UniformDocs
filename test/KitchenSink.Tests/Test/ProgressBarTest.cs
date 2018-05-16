using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
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
    }
}
