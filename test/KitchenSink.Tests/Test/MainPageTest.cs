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

        static string GetAppVersionFromAssemblyFile()
        {
            var Path =  @"src\KitchenSink\Properties\AssemblyInfo.cs";
            var Match = Regex.Match(File.ReadAllText(Path), @"AssemblyFileVersion\(""(.+)""\)");
            var Version = Match.Groups[1].ToString();

            return Version;
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
            Assert.AreEqual(GetAppVersionFromAssemblyFile(), _mainPage.AppVersionSpan.Text);
        }

        [Test]
        public void MainPage_StarcounterVersion()
        {            
            WaitUntil(x => _mainPage.SCVersionSpan.Displayed);
            Assert.AreEqual(Starcounter.Internal.CurrentVersion.Version, _mainPage.SCVersionSpan.Text);
        }
    }
}
