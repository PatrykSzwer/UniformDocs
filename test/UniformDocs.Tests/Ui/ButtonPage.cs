using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class ButtonPage : BasePage
    {
        public ButtonPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/buttonpage-regular-reaction-label']")]
        public IWebElement VegetablesButtonInfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Button (inline script)']")]
        public IWebElement ButtonInlineScript { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Button (function)']")]
        public IWebElement ButtonFunction { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[text() = 'Span (function)']")]
        public IWebElement SpanFunction { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Enable carrot engine']")]
        public IWebElement ButtonEnableCarrotEngine { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Add carrots']")]
        public IWebElement ButtonAddCarrots { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/buttonpage-switch-reaction-label']")]
        public IWebElement EnableCarrotEngineLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/buttonpage-disable-label']")]
        public IWebElement AddCarrotsLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text() = 'Take one Regenerating Carrot']")]
        public IWebElement ButonTakeOneRegeneratingCarrot { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/buttonpage-binding-issue-button']")]
        public IWebElement ButtonBuyCarrot { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/buttonpage-binding-issue-label']")]
        public IWebElement BuyCarrotLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".uniformdocs-regenerating-carrots")]
        public IWebElement GeneratingCarrotsElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".uniformdocs-regenerating-carrots p")]
        public IWebElement GeneratingCarrotsLabel { get; set; }

        public void ClickButtonInlineScript()
        {
            ClickOn(ButtonInlineScript);
        }

        public void ClickButtonFunction()
        {
            ClickOn(ButtonFunction);
        }

        public void ClickSpanFunction()
        {
            ClickOn(SpanFunction);
        }

        public void ClickEnableCarrotEngine()
        {
            ClickOn(ButtonEnableCarrotEngine);
        }

        public void ClickButtonAddCarrots()
        {
            ClickOn(ButtonAddCarrots);
        }

        public void ClickButonTakeOneRegeneratingCarrot()
        {
            ClickOn(ButonTakeOneRegeneratingCarrot);
        }

        public void ClickSpanInsideButtonBuyCarrot()
        {
            ((IJavaScriptExecutor)Driver)
                .ExecuteScript(
                "var span = document.querySelector('.uniformdocs-buy-carrot');"
                + "span.scrollIntoView();"
                + "var rect = span.getBoundingClientRect();"
                + "var hittableElem = document.elementFromPoint(rect.left + 1, rect.top + 1);"
                + "var mousedown = document.createEvent('MouseEvents');"
                + "mousedown.initEvent('mousedown', true, true);"
                + "hittableElem.dispatchEvent(mousedown);"
                + "var click = document.createEvent('MouseEvents');"
                + "click.initEvent('click', true, true);"
                + "hittableElem.dispatchEvent(click);");
        }
    }
}
