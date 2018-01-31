using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class RadioPage : BasePage
    {
        public RadioPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@slot = 'kitchensink/radio-reaction-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//paper-radio-group[@slot = 'kitchensink/radio-paper-radio-group']/descendant::paper-radio-button")]
        public IList<IWebElement> Radios { get; set; }


        public void SelectRadio(string radioName)
        {
            ClickOn(Radios.Single(x => x.Text == radioName)); 
        }
    }
}
