﻿using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    //[TestFixture(Config.Browser.ChromeNoV0)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [Parallelizable(ParallelScope.Fixtures)]
    class CheckboxPageTest : BaseTest
    {
        private CheckboxPage _checkboxPage;
        private MainPage _mainPage;

        public CheckboxPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _checkboxPage = _mainPage.GoToCheckboxPage();
        }

        [Test]
        public void CheckboxPage_CheckboxUncheckedAndCheckedAgain()
        {
            WaitUntil(x => _checkboxPage.Checkbox.Displayed);
            WaitUntil(x => _checkboxPage.InfoLabel.Displayed);
            Assert.IsTrue(WaitForText(_checkboxPage.InfoLabel, "You can drive", 5));
            _checkboxPage.ToggleCheckbox();
            Assert.IsTrue(WaitForText(_checkboxPage.InfoLabel, "You can't drive", 5));
            _checkboxPage.ToggleCheckbox();
            Assert.IsTrue(WaitForText(_checkboxPage.InfoLabel, "You can drive", 5));
        }
        [Test]
        public void CheckboxPage_GitHubSourceURL()
        {
            WaitUntil(x => _checkboxPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
