using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace UniformDocs.Tests.Ui
{
    public class DropdownPage : BasePage
    {
        public DropdownPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/dropdown-pets-dropdown']")]
        public IWebElement PetsSelect { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/dropdown-pets-reaction']")]
        public IWebElement PetLikeLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/dropdown-objects-select']")]
        public IWebElement ProductSelect { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/dropdown-objects-select-label']")]
        public IWebElement JuicySelectLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/dropdown-objects-juicy-select'] select")]
        public IWebElement JuicySelect { get; set; }

        public void SelectPet(string petName)
        {
            SelectElement select = new SelectElement(PetsSelect);
            select.SelectByText(petName);
        }

        public void SelectProduct(string productName)
        {
            SelectElement select = new SelectElement(ProductSelect);
            select.SelectByText(productName);
        }

        public void SelectJuicySelect(string juicyName)
        {
            SelectElement select = new SelectElement(JuicySelect);
            select.SelectByText(juicyName);
        }
    }
}
