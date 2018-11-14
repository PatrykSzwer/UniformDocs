﻿using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

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
    class RadiolistPageTest : BaseTest
    {
        private RadiolistPage _radiolistPage;
        private MainPage _mainPage;

        public RadiolistPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
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
