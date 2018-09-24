using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class TextPage : BasePage
    {
        public TextPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/text-listening-input']")]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/text-typing-listening-input']")]
        public IWebElement InputDynamic { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/text-listening-label']")]
        public IWebElement InputInfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/text-typing-listening-label']")]
        public IWebElement InputInfoLabelDynamic { get; set; }

        public IWebElement PaperInput => Driver.FindElement(By.CssSelector("[slot = 'uniformdocs/text-listening-paper-input']"));

        public IWebElement PaperInputDynamic => Driver.FindElement(By.CssSelector("[slot = 'uniformdocs/text-typing-listening-paper-input']"));

        public IWebElement PaperInputInfoLabel => GetPaperInput(PaperInput, "paper-input-label-1");

        public IWebElement PaperInputDynamicInfoLabel => GetPaperInput(PaperInputDynamic, "paper-input-label-2");


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

        public void ClearPaperInput(IWebElement inputElement)
        {
            var script = "return arguments[0].value = '';";
            ExecuteScriptOnElement(inputElement, script);
            TriggerEventOnElement(inputElement, "change");
        }

        public IWebElement GetInputForPaperElement(IWebElement paperInput)
        {
            return GetShadowElementByQuerySelector(paperInput, "input");
        }

        public IWebElement GetPaperInput(IWebElement paperElement, string id)
        {
            return GetShadowElementByQuerySelector(paperElement, $"#{id}");
        }
    }
}