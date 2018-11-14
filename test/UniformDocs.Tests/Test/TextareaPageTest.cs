using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [TestFixture(Config.Browser.iOS11point2)]
    [TestFixture(Config.Browser.Android7Nougat)]
    [TestFixture(Config.Browser.Android8Oreo)]
    [TestFixture(Config.Browser.iOS11point4)]
    [TestFixture(Config.Browser.iOS12)]
    [TestFixture(Config.Browser.iOS11point3)]
    [TestFixture(Config.Browser.Android7Nougat)]
    [TestFixture(Config.Browser.Android6Marshmello)]
    [TestFixture(Config.Browser.Android8point1Oreo)]
    [TestFixture(Config.Browser.Android7point1Nougat)]
    [TestFixture(Config.Browser.Android5point1Lolliopop)]
    class TextareaPageTest : BaseTest
    {
        private TextareaPage _textareaPage;
        private MainPage _mainPage;

        public TextareaPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _textareaPage = _mainPage.GoToTextareaPage();
        }

        [Test]
        public void TextareaPage_WriteToTextArea()
        {
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
            WaitUntil(x => _textareaPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
