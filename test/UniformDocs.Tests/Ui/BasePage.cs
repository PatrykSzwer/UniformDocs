using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace UniformDocs.Tests.Ui
{
    public class BasePage
    {
        public IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.TagName, Using = "github-source-links")]
        public IWebElement GitHubSourceLinks { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'MainPage']")]
        public IWebElement MainPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Datepicker']")]
        public IWebElement DatepickerPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Client Local State']")]
        public IWebElement ClientLocalStateLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Dropdown']")]
        public IWebElement DropdownPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Nested Views']")]
        public IWebElement NestedPartialsPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Table']")]
        public IWebElement TablePageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Link']")]
        public IWebElement UrlPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Button']")]
        public IWebElement ButtonPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Card']")]
        public IWebElement CardPageLink { get; set; }


        [FindsBy(How = How.XPath, Using = "//a[text() = 'Checkbox']")]
        public IWebElement CheckboxPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Text Input']")]
        public IWebElement TextPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Textarea']")]
        public IWebElement TextareaPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Redirect']")]
        public IWebElement RedirectPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Password']")]
        public IWebElement PasswordPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Progress Bar']")]
        public IWebElement ProgressBarPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Pagination']")]
        public IWebElement PaginationPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Autocomplete']")]
        public IWebElement AutoCompletePageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'File Upload']")]
        public IWebElement FileUploadPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Toggle Button']")]
        public IWebElement ToggleButtonPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Breadcrumb']")]
        public IWebElement BreadcrumbPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Multiselect']")]
        public IWebElement MultiselectPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Radio Button']")]
        public IWebElement RadioPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Radiolist']")]
        public IWebElement RadiolistPageLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Markdown']")]
        public IWebElement MarkdownPageLink { get; set; }

        public IWebElement WaitForElementToBeClickable(IWebElement elementName, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.ElementToBeClickable(elementName));
        }

        public void ClickOn(IWebElement elementName, int seconds = 10)
        {
            IWebElement element = WaitForElementToBeClickable(elementName, seconds);
            element.Click();
        }

        public void DblClickOn(IWebElement elementName, int seconds = 10)
        {
            IWebElement element = WaitForElementToBeClickable(elementName, seconds);
            element.Click();
            element.Click();
        }

        public object ExecuteScriptOnElement(IWebElement shadowRootElement, string script)
        {
            var scriptResult = ((IJavaScriptExecutor)Driver)
                .ExecuteScript(script, shadowRootElement);

            return scriptResult;
        }

        public IWebElement GetShadowElementByQuerySelector(By elementSelector, string queryArgument)
        {
            var shadowRootElement = Driver.FindElement(elementSelector);
            return GetShadowElementByQuerySelector(shadowRootElement, queryArgument);
        }

        public IWebElement GetShadowElementByQuerySelector(IWebElement shadowRootElement, string queryArgument)
        {
            var script = $"return arguments[0].shadowRoot.querySelector('{queryArgument}')";
            var webElement = (IWebElement)ExecuteScriptOnElement(shadowRootElement, script);

            return webElement;
        }

        public void ScrollToTheTop()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");
        }
    }
}
