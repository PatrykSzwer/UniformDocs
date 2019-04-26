using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private ToggleButtonPage _toggleButtonPage;

        public void InitToggleButtonPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _toggleButtonPage = _mainPage.GoToToggleButtonPage();
        }

        [Test]
        public void ToggleButtonPage_CheckboxUncheckedAndCheckedAgain()
        {
            InitToggleButtonPageTest();
            WaitUntil(x => _toggleButtonPage.ToogleButton.Displayed);
            WaitUntil(x => _toggleButtonPage.InfoLabel.Displayed);
            Assert.AreEqual("I accept terms and conditions", _toggleButtonPage.InfoLabel.Text);
            _toggleButtonPage.ChangeToggleButtonState();
            Assert.IsTrue(WaitForText(_toggleButtonPage.InfoLabel, "I don't accept terms and conditions", 5));
            _toggleButtonPage.ChangeToggleButtonState();
            Assert.IsTrue(WaitForText(_toggleButtonPage.InfoLabel, "I accept terms and conditions", 5));
        }
        [Test]
        public void ToggleButtonPage_GitHubSourceURL()
        {
            InitToggleButtonPageTest();
            WaitUntil(x => _toggleButtonPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
