using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private CheckboxPage _checkboxPage;

        [SetUp]
        public void InitCheckboxPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _checkboxPage = _mainPage.GoToCheckboxPage();
        }

        [Test]
        public void CheckboxPage_CheckboxUncheckedAndCheckedAgain()
        {
            InitCheckboxPageTest();
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
            InitCheckboxPageTest();
            WaitUntil(x => _checkboxPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
