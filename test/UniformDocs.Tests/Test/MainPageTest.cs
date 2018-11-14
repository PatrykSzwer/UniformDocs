using System.Drawing.Design;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Text.RegularExpressions;

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
    class MainPageTest : BaseTest
    {
        private MainPage _mainPage;

        static string GetAppVersionFromEndPoint()
        {
            return new System.Net.WebClient().DownloadString(Config.TestedAppUrl + "/uniformdocs-app-version");
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
            Assert.True(Regex.IsMatch(_mainPage.SCVersionSpan.Text, @"\d+\.\d+\.\d+.\d+"));
        }
    }
}
