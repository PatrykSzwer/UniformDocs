using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class RadioPage : BasePage
    {
        public RadioPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/radio-reaction-label']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot='uniformdocs/radio-paper-radio-group'] > paper-radio-button")]
        public IList<IWebElement> Radios { get; set; }


        public void SelectRadio(string radioName)
        {
            ClickOn(Radios.Single(x => x.Text == radioName)); 
        }
    }
}
