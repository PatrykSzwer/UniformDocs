using System.Drawing.Design;
using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Text.RegularExpressions;

namespace KitchenSink.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class MainPageTest : BaseTest
    {
        private MainPage _mainPage;

        static string GetAppVersionFromEndPoint()
        {
            return new System.Net.WebClient().DownloadString(Config.KitchenSinkUrl + "/kitchensink-app-version");
        }

        public MainPageTest(Config.Browser browser) : base(browser)
        {
        }


        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
        }

        [Test]
        public void MainPage_AppVersion()
        {            
            WaitUntil(x => _mainPage.AppVersionSpan.Displayed);
            Assert.AreEqual(GetAppVersionFromEndPoint(), _mainPage.AppVersionSpan.Text);

            // make sure it's a semver
            Assert.True(Regex.IsMatch(GetAppVersionFromEndPoint(), @"\d+\.\d+\.\d+"));
        }

        [Test]
        public void MainPage_StarcounterVersion()
        {            
            WaitUntil(x => _mainPage.SCVersionSpan.Displayed);
            Assert.AreEqual(Starcounter.Internal.CurrentVersion.Version, _mainPage.SCVersionSpan.Text);
        }
    }
}
