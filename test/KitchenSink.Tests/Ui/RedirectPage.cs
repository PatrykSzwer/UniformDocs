using KitchenSink.Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class RedirectPage : BasePage
    {
        public RedirectPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/redirect-current-partial-fruit-button']")]
        public IWebElement FruitButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/redirect-current-partial-vegetable-button']")]
        public IWebElement VegetableButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/redirect-current-partial-bread-button']")]
        public IWebElement BreadButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/redirect-current-partial-favorite-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/redirect-another-partial-button']")]
        public IWebElement MorphButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/redirect-external-link-button']")]
        public IWebElement RedirectButton { get; set; }

        public void ClickButton(Config.Buttons button)
        {
            switch (Config.ButtonsDictionary[button])
            {
                case "Bread":
                    ClickOn(BreadButton); break;
                case "Vegetable":
                    ClickOn(VegetableButton); break;
                case "Fruit":
                    ClickOn(FruitButton); break;
                case "Morph":
                    ClickOn(MorphButton); break;
                case "Redirect":
                    ClickOn(RedirectButton); break;
            }
        }
    }
}