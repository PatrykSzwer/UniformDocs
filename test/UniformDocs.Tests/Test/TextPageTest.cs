using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    internal class TextPageTest : BaseTest
    {
        private TextPage _textPage;
        private MainPage _mainPage;

        public TextPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _textPage = _mainPage.GoToTextPage();
        }

        [Test]
        public void TextPage_TextPropagationOnUnfocus()
        {
            WaitUntil(x => _textPage.Input.Displayed);

            _textPage.FillInput(_textPage.Input, "Krystian");
            Assert.IsTrue(WaitForText(_textPage.InputInfoLabel, "Hi, Krystian!", 5));
            _textPage.ClearInput(_textPage.Input);
            WaitUntil(x => _textPage.Input.Text == string.Empty);
            Assert.AreEqual("What's your name?", _textPage.InputInfoLabel.Text);
        }

        [Test]
        public void TextPage_TextPropagationWhileTyping()
        {
            WaitUntil(x => _textPage.InputDynamic.Displayed);

            _textPage.FillInput(_textPage.InputDynamic, "Krystian");
            Assert.IsTrue(WaitForText(_textPage.InputInfoLabelDynamic, "Hi, Krystian!", 5));
            _textPage.ClearInput(_textPage.InputDynamic);
            WaitUntil(x => _textPage.InputDynamic.Text == string.Empty);
            WaitUntil(x => "What's your name?" == _textPage.InputInfoLabelDynamic.Text, 
                $"Expected: What's your name?, but was: {_textPage.InputInfoLabelDynamic.Text}");
        }

        [Test]
        public void TextPage_TextPropagationForPaperTextOnUnfocus()
        {
            // Make sure that paper element is loaded.
            WaitUntil(x => _textPage.PaperInput.Displayed);

            var shadowInput = _textPage.GetInputForPaperElement(_textPage.PaperInput);

            _textPage.FillInput(shadowInput, "Krystian");
            Assert.IsTrue(WaitForText(_textPage.PaperInputInfoLabel, "Hi, Krystian!", 5));
            _textPage.ClearPaperInput(_textPage.PaperInput);
            WaitUntil(x => shadowInput.Text == string.Empty);
            WaitUntil(x => "What's your name?" == _textPage.PaperInputInfoLabel.Text,
                $"Expected: What's your name?, but was: {_textPage.PaperInputInfoLabel.Text}");
        }

        [Test]
        public void TextPage_TextPropagationForPaperTextWhileTyping()
        {
            // Make sure that paper element is loaded.
            WaitUntil(x => _textPage.PaperInputDynamic.Displayed);

            var shadowInput = _textPage.GetInputForPaperElement(_textPage.PaperInputDynamic);

            _textPage.FillInput(shadowInput, "Krystian");
            Assert.IsTrue(WaitForText(_textPage.PaperInputDynamicInfoLabel, "Hi, Krystian!", 5));
            _textPage.ClearInput(shadowInput);
            WaitUntil(x => shadowInput.Text == string.Empty);
            Assert.AreEqual("What's your name?", _textPage.PaperInputDynamicInfoLabel.Text);
        }
        [Test]
        public void TextPage_GitHubSourceURL()
        {
            WaitUntil(x => _textPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}