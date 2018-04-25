using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;

namespace KitchenSink.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class RedirectPageTest : BaseTest
    {
        private RedirectPage _redirectPage;
        private MainPage _mainPage;

        public RedirectPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _redirectPage = _mainPage.GoToRedirectPage();
        }

        [Test]
        public void RedirectPage_ClickingOnFruitShouldChangeUrlAndText()
        {
            _redirectPage.ClickButton(Config.Buttons.Fruit);
            Assert.IsTrue(WaitForText(_redirectPage.InfoLabel, "You've got some tasty apple", 20));
            Assert.AreEqual(Config.KitchenSinkUrl + "/Redirect/apple", Driver.Url);

            _redirectPage.ClickButton(Config.Buttons.Vegetable);
            Assert.IsTrue(WaitForText(_redirectPage.InfoLabel, "You've got some tasty carrot", 20));
            Assert.AreEqual(Config.KitchenSinkUrl + "/Redirect/carrot", Driver.Url);

            _redirectPage.ClickButton(Config.Buttons.Bread);
            Assert.IsTrue(WaitForText(_redirectPage.InfoLabel, "You've got some tasty baguette", 20));
            Assert.AreEqual(Config.KitchenSinkUrl + "/Redirect/baguette", Driver.Url);
        }

        [Test]
        public void RedirectPage_ClickingOnRedirectToAnotherPartialShouldChangeUrl()
        {
            _redirectPage.ClickButton(Config.Buttons.Morph);
            WaitUntil(x => Driver.Url == Config.KitchenSinkUrl.ToString());
            Assert.AreEqual(Config.KitchenSinkUrl.ToString(), Driver.Url);
        }

        [Test]
        public void RedirectPage_ClickingOnRedirectToExternalWebsiteShouldChangeUrl()
        {
            _redirectPage.ClickButton(Config.Buttons.Redirect);
            WaitUntil(x => Driver.Url == "https://starcounter.io/");
            Assert.AreEqual("https://starcounter.io/", Driver.Url);
        }
    }
}
