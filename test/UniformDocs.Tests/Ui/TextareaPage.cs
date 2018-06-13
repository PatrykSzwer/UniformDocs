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
            Textarea.SendKeys(input);
        }

        public void ClearTextarea()
        {
            // can't use Clear()
            var textAreaLength = Textarea.GetAttribute("value").Length;

            for (var i = 0; i < textAreaLength; i++)
            {
                Textarea.SendKeys(Keys.Backspace);
            }
        }
    }
}