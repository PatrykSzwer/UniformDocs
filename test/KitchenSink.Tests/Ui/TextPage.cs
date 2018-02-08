using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class TextPage : BasePage
    {
        public TextPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/text-listening-input']")]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/text-typing-listening-input']")]
        public IWebElement InputDynamic { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/text-listening-label']")]
        public IWebElement InputInfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'kitchensink/text-typing-listening-label']")]
        public IWebElement InputInfoLabelDynamic { get; set; }

        public IWebElement PaperInput => Driver.FindElement(By.CssSelector("[slot = 'kitchensink/text-listening-paper-input']"));

        public IWebElement PaperInputDynamic => Driver.FindElement(By.CssSelector("[slot = 'kitchensink/text-typing-listening-paper-input']"));

        public IWebElement PaperInputInfoLabel => ExpandShadowRoot(PaperInput).FindElement(By.Id("paper-input-label-1"));

        public IWebElement PaperInputDynamicInfoLabel => ExpandShadowRoot(PaperInputDynamic).FindElement(By.Id("paper-input-label-2"));


        public void FillInput(IWebElement inputElement, string input)
        {
            inputElement.Clear();
            inputElement.SendKeys(input);
            inputElement.SendKeys(Keys.Enter);
        }

        public void ClearInput(IWebElement inputElement)
        {
            var inputLength = inputElement.GetAttribute("value").Length;

            for (var i = 0; i < inputLength; i++)
            {
                inputElement.SendKeys(Keys.Backspace);
            }

            inputElement.SendKeys(Keys.Enter);
        }

        public IWebElement GetInputForPaperElement(IWebElement paperInput)
        {
            return ExpandShadowRoot(paperInput).FindElement(By.CssSelector("input"));
        }

        public IWebElement GetLabelForPaperElement(IWebElement paperElement)
        {
            var shadowRoot = ExpandShadowRoot(paperElement);
            var content = ExpandShadowRoot(shadowRoot);
            return content.FindElement(By.XPath("//label"));
        }
    }
}