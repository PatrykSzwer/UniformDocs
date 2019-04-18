using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.ChromeNoV0)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class TablePageTest : BaseTest
    {
        private TablePage _tablePage;
        private MainPage _mainPage;

        public TablePageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _mainPage = new MainPage(Driver).GoToMainPage();
            _tablePage = _mainPage.GoToTablePage();
        }

        [Test]
        public void TablePage_AddNewRow()
        {
            WaitUntil(x => _tablePage.PetsTable.Displayed);
            _tablePage.AddPet();
            WaitUntil(x => _tablePage.PetsTableRows.Count == 4);
        }
        [Test]
        public void TablePage_GitHubSourceURL()
        {
            WaitUntil(x => _tablePage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
