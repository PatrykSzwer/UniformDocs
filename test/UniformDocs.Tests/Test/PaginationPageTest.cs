using System.Linq;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class PaginationPageTest : BaseTest
    {
        private PaginationPage _paginationPage;
        private MainPage _mainPage;

        public PaginationPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _paginationPage = _mainPage.GoToPaginationPage();
        }

        [Test]
        public void PaginationPage_Dropdown_HasCorrectOptions()
        {
            _paginationPage.ScrollToTheTop();

            WaitUntil(x => _paginationPage.DropDown.Displayed);
            _paginationPage.DropdownSelect("Show 5 items per page");
            Assert.IsTrue(WaitForText(_paginationPage.PaginationInfoLabel, "page 1 of 20", 5));
            Assert.AreEqual(5, _paginationPage.PaginationResult.Count);
            _paginationPage.DropdownSelect("Show 15 items per page");
            Assert.IsTrue(WaitForText(_paginationPage.PaginationInfoLabel, "page 1 of 7", 5));
            Assert.AreEqual(15, _paginationPage.PaginationResult.Count);
            _paginationPage.DropdownSelect("Show 30 items per page");
            Assert.IsTrue(WaitForText(_paginationPage.PaginationInfoLabel, "page 1 of 4", 5));
            Assert.AreEqual(30, _paginationPage.PaginationResult.Count);
        }


        [Test]
        public void PaginationPage_LastButton_GoesToLastPage()
        {
            _paginationPage.ScrollToTheTop();

            WaitUntil(x => _paginationPage.DropDown.Displayed);
            _paginationPage.DropdownSelect("Show 15 items per page");
            WaitForText(_paginationPage.PaginationInfoLabel, "page 1 of 7", 5);
            WaitUntil(x => _paginationPage.PaginationResult.Count == 15);
            _paginationPage.GoToPage("Last");
            WaitUntil(y => _paginationPage.PaginationResult.Where(x => x.Text.Contains("99")).
                Select(x => x.Text).First() == "Arbitrary Book 99 Arbitrary Author");
            _paginationPage.GoToPage("3");
            WaitUntil(y => _paginationPage.PaginationResult.Where(x => x.Text.Contains("40")).
                Select(x => x.Text).First() == "Arbitrary Book 40 Arbitrary Author");
            _paginationPage.GoToPage("First");
            WaitUntil(y => _paginationPage.PaginationResult.Where(x => x.Text.Contains("1")).
                Select(x => x.Text).First() == "Arbitrary Book 1 Arbitrary Author");
        }
        [Test]
        public void PaginationPage_GitHubSourceURL()
        {
            WaitUntil(x => _paginationPage.GitHubSourceLinks.Displayed, "", 10, 3);
            TestGitHubSourceLinkURLs();
        }
    }
}
