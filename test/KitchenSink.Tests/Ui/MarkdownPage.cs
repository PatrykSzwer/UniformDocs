using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class MarkdownPage : BasePage
    {
        public MarkdownPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.TagName, Using = "marked-element")]
        public IWebElement MarkedElement { get; set; }

        public string GetHeaderText()
        {
            return GetMarkedElement().Text;
        }

        private IWebElement GetMarkedElement()
        {
            return GetShadowElementByQuerySelector(By.TagName("marked-element"), "h1");
        }
    }
}
