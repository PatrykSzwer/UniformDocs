using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private PasswordPage _passwordPage;

        public void InitPasswordPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _passwordPage = _mainPage.GoToPasswordPage();
        }

        [Test]
        public void PasswordPage_PasswordTooShort()
        {
            InitPasswordPageTest();
            const string originalLabel = "Password must be at least 6 chars long";
            const string password = "123";

            WaitUntil(x => _passwordPage.PasswordInput.Displayed);
            _passwordPage.ClearPassword();
            _passwordPage.FillPassword(password);
            Assert.IsTrue(WaitForText(_passwordPage.PaswordInputInfoLabel, originalLabel, 5));
        }

        [Test]
        public void PasswordPage_PasswordWithProperLength()
        {
            InitPasswordPageTest();
            const string password = "123456";

            WaitUntil(x => _passwordPage.PasswordInput.Displayed);
            _passwordPage.ClearPassword();
            _passwordPage.FillPassword(password);
            Assert.IsTrue(WaitForText(_passwordPage.PaswordInputInfoLabel, "Good password!", 5));
        }

        [Test]
        public void PasswordPage_ChangingPasswordToGoodThenToShort()
        {
            InitPasswordPageTest();
            const string password = "123456";

            WaitUntil(x => _passwordPage.PasswordInput.Displayed);
            _passwordPage.ClearPassword();
            _passwordPage.FillPassword(password);
            Assert.IsTrue(WaitForText(_passwordPage.PaswordInputInfoLabel, "Good password!", 5));
            _passwordPage.ClearPassword();
            _passwordPage.FillPassword("123");
            Assert.IsTrue(WaitForText(_passwordPage.PaswordInputInfoLabel, "Password must be at least 6 chars long", 5));
        }
        [Test]
        public void PasswordPage_GitHubSourceURL()
        {
            InitPasswordPageTest();
            WaitUntil(x => _passwordPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
