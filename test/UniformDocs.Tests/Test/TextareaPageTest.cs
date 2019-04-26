using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private TextareaPage _textareaPage;

        public void InitTextareaPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _textareaPage = _mainPage.GoToTextareaPage();
        }

        [Test]
        public void TextareaPage_WriteToTextArea()
        {
            InitTextareaPageTest();
            const string newText = "We all love princess cake!";

            WaitUntil(x => _textareaPage.Textarea.Displayed);
            _textareaPage.ClearTextarea();
            WaitUntil(x => _textareaPage.Textarea.GetAttribute("text-value") != string.Empty);
            _textareaPage.FillTextarea(newText);
            Assert.IsTrue(WaitForText(_textareaPage.TextareaInfoLabel, "Length of your bio: 26 chars", 5));
        }

        [Test]
        public void TextareaPage_CounterPropagationWhileTyping()
        {
            InitTextareaPageTest();
            const string newText = "Love";

            WaitUntil(x => _textareaPage.Textarea.Displayed);
            _textareaPage.ClearTextarea();
            _textareaPage.FillTextarea(newText);
            Assert.IsTrue(WaitForText(_textareaPage.TextareaInfoLabel, "Length of your bio: 4 chars", 5));
            _textareaPage.ClearTextarea();
            Assert.IsTrue(WaitForText(_textareaPage.TextareaInfoLabel, "Length of your bio: 0 chars", 5));
        }
        [Test]
        public void TextareaPage_GitHubSourceURL()
        {
            InitTextareaPageTest();
            WaitUntil(x => _textareaPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
