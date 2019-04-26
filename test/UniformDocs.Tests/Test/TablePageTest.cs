using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private TablePage _tablePage;

        public void InitTablePageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _tablePage = _mainPage.GoToTablePage();
        }

        [Test]
        public void TablePage_AddNewRow()
        {
            InitTablePageTest();
            WaitUntil(x => _tablePage.PetsTable.Displayed);
            _tablePage.AddPet();
            WaitUntil(x => _tablePage.PetsTableRows.Count == 4);
        }
        [Test]
        public void TablePage_GitHubSourceURL()
        {
            InitTablePageTest();
            WaitUntil(x => _tablePage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
