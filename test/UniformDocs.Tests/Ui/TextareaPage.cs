using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class TextareaPage : BasePage
    {
        public TextareaPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/textarea-textarea']")]
        public IWebElement Textarea { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/textarea-label']")]
        public IWebElement TextareaInfoLabel { get; set; }

        public void FillTextarea(string input)
        {
            //Textarea.SendKeys(input); does not work well in Chrome
            var script = "return arguments[0].value = '" + input + "';";
            ExecuteScriptOnElement(Textarea, script);
            TriggerEventOnElement(Textarea, "input");
        }

        public void ClearTextarea()
        {
            var script = "return arguments[0].value = '';";
            ExecuteScriptOnElement(Textarea, script);
            TriggerEventOnElement(Textarea, "input");
        }
    }
}