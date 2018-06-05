using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class DatagridPageTest : BaseTest
    {
        private DatagridPage _datagridPage;
        private MainPage _mainPage;
        private readonly Config.Browser _browser;

        public DatagridPageTest(Config.Browser browser) : base(browser)
        {
            _browser = browser;
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _datagridPage = _mainPage.GoToDataGridPage();
        }

        [Test]
        public void TablePage_AddNewRow()
        {
            WaitUntil(x => _datagridPage.CheckTableVisible());
            _datagridPage.AddPet();
            WaitUntil(x => _datagridPage.GetTableRowsCount() == 4);
        }

        [Test]
        public void TablePage_ChangeDogToCatFeature()
        {
            WaitUntil(x => _datagridPage.CheckTableVisible());
            Assert.AreEqual(1, _datagridPage.GetCatsCount());
            Assert.AreEqual(1, _datagridPage.GetMeowsCount());
            _datagridPage.ReplaceTextInACell("Dog", "Cat");
            WaitUntil(x => _datagridPage.GetMeowsCount() == 2);
            Assert.AreEqual(2, _datagridPage.GetCatsCount());
            Assert.AreEqual(2, _datagridPage.GetMeowsCount());
        }
        [Test]
        public void TablePage_GitHubSourceURL()
        {
            WaitUntil(x => _datagridPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
