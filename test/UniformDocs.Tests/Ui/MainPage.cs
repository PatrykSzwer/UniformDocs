using UniformDocs.Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class MainPage : BasePage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "[slot='uniformdocs/main-app-version'] span:nth-child(1)")]
        public IWebElement AppVersionSpan { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot='uniformdocs/main-app-version'] span:nth-child(2)")]
        public IWebElement SCVersionSpan { get; set; }

        public MainPage GoToMainPage()
        {
            Driver.Navigate().GoToUrl(Config.UniformDocsUrl + "/MainPage");
            return this;
        }

        public DatepickerPage GoToDatePickerPage()
        {
            ClickOn(DatepickerPageLink);
            return new DatepickerPage(Driver);
        }

        public DropdownPage GoToDropdownPage()
        {
            ClickOn(DropdownPageLink);
            return new DropdownPage(Driver);
        }

        public NestedPartialsPage GoToNestedPartialsPage()
        {
            ClickOn(NestedPartialsPageLink);
            return new NestedPartialsPage(Driver);
        }

        public TablePage GoToTablePage()
        {
            ClickOn(TablePageLink);
            return new TablePage(Driver);
        }
        public UrlPage GoToUrlPage()
        {
            ClickOn(UrlPageLink);
            return new UrlPage(Driver);
        }

        public CheckboxPage GoToCheckboxPage()
        {
            ClickOn(CheckboxPageLink);
            return new CheckboxPage(Driver);
        }

        public ButtonPage GoToButtonPage()
        {
            ClickOn(ButtonPageLink);
            return new ButtonPage(Driver);
        }

        public CardPage GoToCardPage()
        {
            ClickOn(CardPageLink);
            return new CardPage(Driver);

        }
        public TextPage GoToTextPage()
        {
            ClickOn(TextPageLink);
            return new TextPage(Driver);
        }

        public PaginationPage GoToPaginationPage()
        {
            ClickOn(PaginationPageLink);
            return new PaginationPage(Driver);
        }

        public AutoCompletePage GoToAutoCompletePage()
        {
            ClickOn(AutoCompletePageLink);
            return new AutoCompletePage(Driver);
        }

        public ClientLocalStatePage GoToClientLocalStatePage()
        {
            ClickOn(ClientLocalStateLink);
            return new ClientLocalStatePage(Driver);
        }

        public PasswordPage GoToPasswordPage()
        {
            ClickOn(PasswordPageLink);
            return new PasswordPage(Driver);
        }

        public ProgressBarPage GoToProgressBarPage()
        {
            ClickOn(ProgressBarPageLink);
            return new ProgressBarPage(Driver);
        }

        public RedirectPage GoToRedirectPage()
        {
            ClickOn(RedirectPageLink);
            return new RedirectPage(Driver);
        }

        public TextareaPage GoToTextareaPage()
        {
            ClickOn(TextareaPageLink);
            return new TextareaPage(Driver);
        }

        public FileUploadPage GoToFileUploadPage()
        {
            ClickOn(FileUploadPageLink);
            return new FileUploadPage(Driver);
        }

        public ToggleButtonPage GoToToggleButtonPage()
        {
            ClickOn(ToggleButtonPageLink);
            return new ToggleButtonPage(Driver);
        }

        public DatagridPage GoToDataGridPage()
        {
            ClickOn(DataGridPageLink);
            return new DatagridPage(Driver);
        }

        public RadioPage GoToRadioPage()
        {
            ClickOn(RadioPageLink);
            return new RadioPage(Driver);
        }

        public RadiolistPage GoToRadiolistPage()
        {
            ClickOn(RadiolistPageLink);
            return new RadiolistPage(Driver);
        }

        public MarkdownPage GoToMarkdownPage()
        {
            ClickOn(MarkdownPageLink);
            return new MarkdownPage(Driver);
        }
    }
}
