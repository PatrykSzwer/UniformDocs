﻿using System.Linq;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

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
    class NestedPartialsPageTest : BaseTest
    {
        private NestedPartialsPage _nestedPartialsPage;
        private MainPage _mainPage;

        public NestedPartialsPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _nestedPartialsPage = _mainPage.GoToNestedPartialsPage();
        }

        [Test]
        public void NestedPartialsPage_AddNewChild()
        {
            WaitUntil(x => _nestedPartialsPage.ChildCompositions.Count > 0);

            var compositionsBefore = _nestedPartialsPage.ChildCompositions.Count;
            _nestedPartialsPage.AddChild();
            WaitUntil(x => _nestedPartialsPage.ChildCompositions.Count > compositionsBefore);
            var compositionsAfter = _nestedPartialsPage.ChildCompositions.Count;

            Assert.Greater(compositionsAfter, compositionsBefore);
        }

        [Test]
        public void NestedPartialsPage_AddNewChildAndSubchild()
        {
            WaitUntil(x => _nestedPartialsPage.ChildCompositions.Count > 0);
            var compositionsBefore = _nestedPartialsPage.ChildCompositions.Count;

            _nestedPartialsPage.AddChild();
            WaitUntil(x => _nestedPartialsPage.ChildCompositions.Count > compositionsBefore);
            var compositionsCurrent = _nestedPartialsPage.ChildCompositions.Count;

            _nestedPartialsPage.Driver.FindElements(By.XPath("//button[text() = 'Add child']")).Last().Click();
            WaitUntil(x => _nestedPartialsPage.ChildCompositions.Count > compositionsCurrent);

            var compositionsAfter = _nestedPartialsPage.ChildCompositions.Count;

            WaitUntil(x => compositionsBefore + 2 == compositionsAfter, $"Expected: {compositionsAfter}, but was: {compositionsBefore + 2}");
        }
        [Test]
        public void NestedPartialsPage_GitHubSourceURL()
        {
            WaitUntil(x => _nestedPartialsPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
