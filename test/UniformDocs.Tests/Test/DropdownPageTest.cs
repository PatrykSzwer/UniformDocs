using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private DropdownPage _dropDownPage;

        private const string PolymerLibrary = "Polymer JavaScript library";
        private const string StarcounterDatabase = "Starcounter Database";

        public void InitDropdownPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _dropDownPage = _mainPage.GoToDropdownPage();
        }

        [Test]
        public void DropdownPage_PetsDropdown_SelectPets()
        {
            InitDropdownPageTest();
            WaitUntil(x => _dropDownPage.PetsSelect.Displayed);
            _dropDownPage.SelectPet("dogs");
            Assert.IsTrue(WaitForText(_dropDownPage.PetLikeLabel, "You like dogs", 5));
            _dropDownPage.SelectPet("cats");
            Assert.IsTrue(WaitForText(_dropDownPage.PetLikeLabel, "You like cats", 5));
            _dropDownPage.SelectPet("rabbit");
            Assert.IsTrue(WaitForText(_dropDownPage.PetLikeLabel, "You like rabbit", 5));
        }

        [Test]
        public void DropdownPage_JuicyDropdown_SelectProduct()
        {
            InitDropdownPageTest();
            WaitUntil(x => _dropDownPage.JuicySelect.Displayed);
            Assert.AreEqual(PolymerLibrary, new SelectElement(_dropDownPage.ProductSelect).SelectedOption.Text);

            _dropDownPage.SelectJuicySelect(PolymerLibrary);
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Polymer JavaScript library", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.ProductSelect).SelectedOption, "Polymer JavaScript library", 5));

            _dropDownPage.SelectJuicySelect(StarcounterDatabase);
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Starcounter Database", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.ProductSelect).SelectedOption, "Starcounter Database", 5));
        }

        [Test]
        public void DropdownPage_Dropdown_SelectProduct()
        {
            InitDropdownPageTest();
            WaitUntil(x => _dropDownPage.ProductSelect.Displayed);
            _dropDownPage.SelectProduct(StarcounterDatabase);
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Starcounter Database", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.JuicySelect).SelectedOption, "Starcounter Database", 5));

            _dropDownPage.SelectProduct(PolymerLibrary);
            Assert.IsTrue(WaitForText(_dropDownPage.JuicySelectLabel, "You have selected: Polymer JavaScript library", 5));
            Assert.IsTrue(WaitForText(new SelectElement(_dropDownPage.JuicySelect).SelectedOption, "Polymer JavaScript library", 5));
        }
        [Test]
        public void DropdownPage_GitHubSourceURL()
        {
            InitDropdownPageTest();
            WaitUntil(x => _dropDownPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}

