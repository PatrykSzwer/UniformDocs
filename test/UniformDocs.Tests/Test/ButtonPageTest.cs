using System.Drawing.Design;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;


namespace UniformDocs.Tests.Test
{

    partial class AllTests : BaseTest
    {
        private ButtonPage _buttonPage;

        public void InitButtonPage()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _buttonPage = _mainPage.GoToButtonPage();
        }

        [Test]
        public void ButtonPage_RegularButton()
        {
            InitButtonPage();

            WaitUntil(x => _buttonPage.VegetablesButtonInfoLabel.Displayed);
            Assert.AreEqual("You don't have any carrots", _buttonPage.VegetablesButtonInfoLabel.Text);

            WaitUntil(x => _buttonPage.ButtonInlineScript.Displayed);
            _buttonPage.ClickButtonInlineScript();
            Assert.IsTrue(WaitForText(_buttonPage.VegetablesButtonInfoLabel, "You have 1 imaginary carrots", 5));

            WaitUntil(x => _buttonPage.ButtonFunction.Displayed);
            _buttonPage.ClickButtonFunction();
            Assert.IsTrue(WaitForText(_buttonPage.VegetablesButtonInfoLabel, "You have 2 imaginary carrots", 5));

            WaitUntil(x => _buttonPage.SpanFunction.Displayed);
            _buttonPage.ClickSpanFunction();
            Assert.IsTrue(WaitForText(_buttonPage.VegetablesButtonInfoLabel, "You have 3 imaginary carrots", 5));
        }

        [Test]
        public void ButtonPage_SelfButton()
        {
            InitButtonPage();

            _buttonPage.ClickButonTakeOneRegeneratingCarrot();
            WaitUntil(x => _buttonPage.GeneratingCarrotsElement.Displayed);
            Assert.IsTrue(WaitForText(_buttonPage.GeneratingCarrotsLabel, "This button will regenerate in 5 seconds!", 5));
        }

        [Test]
        public void ButtonPage_SwitchButton()
        {
            InitButtonPage();

            Assert.IsTrue(WaitForText(_buttonPage.EnableCarrotEngineLabel, "Carrot engine is off", 5));

            _buttonPage.ClickEnableCarrotEngine();
            Assert.IsTrue(WaitForText(_buttonPage.EnableCarrotEngineLabel, "Carrot engine is on", 5));
            _buttonPage.ClickEnableCarrotEngine();
            Assert.IsTrue(WaitForText(_buttonPage.EnableCarrotEngineLabel, "Carrot engine is off", 5));
        }

        [Test]
        public void ButtonPage_DisabledButton()
        {
            InitButtonPage();

            Assert.IsTrue(WaitForText(_buttonPage.AddCarrotsLabel,
                "You don't have any carrots", 5));
            _buttonPage.ClickButtonAddCarrots();
            Assert.IsTrue(WaitForText(_buttonPage.AddCarrotsLabel,
                "You have 1 imaginary carrots", 5));
        }

        [Test]
        public void ButtonPage_ButtonWithChildren()
        {
            InitButtonPage();

            Assert.IsTrue(WaitForText(_buttonPage.BuyCarrotLabel,
                "You don't have any carrots", 5));
            _buttonPage.ClickSpanInsideButtonBuyCarrot();
            Assert.IsTrue(WaitForText(_buttonPage.BuyCarrotLabel,
                "You bought a carrot!", 5));
        }
        [Test]
        public void ButtonPage_GitHubSourceURL()
        {
            InitButtonPage();

            WaitUntil(x => _buttonPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
