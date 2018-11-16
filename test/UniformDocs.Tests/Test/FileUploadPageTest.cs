﻿using UniformDocs.Tests.Ui;
using NUnit.Framework;
using System.IO;
using UniformDocs.Tests.Utilities;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    class FileUploadPageTest : BaseTest
    {
        private FileUploadPage _fileUploadPage;
        private MainPage _mainPage;
        private readonly Config.Browser _browser;

        public FileUploadPageTest(Config.Browser browser) : base(browser)
        {
            _browser = browser;
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _fileUploadPage = _mainPage.GoToFileUploadPage();
        }

        [Test]
        public void FileUploadPage_UploadAFile()
        {
            WaitUntil(x => _fileUploadPage.CheckFileInputVisible());

            string tempFilePath = Path.GetTempFileName();
            using (StreamWriter outputFile = new StreamWriter(tempFilePath, false))
            {
                outputFile.WriteLine("Test123");
            }
            _fileUploadPage.UploadAFile(tempFilePath);

            WaitUntil(x => _fileUploadPage.GetUploadedFilesCount() > 0);

            Assert.AreEqual("Do not forget to delete files from your temporary folder!",
                _fileUploadPage.InfoLabel.Text);
        }

        [Test]
        public void FileUploadPage_UploadAndDeleteAFile()
        {
            WaitUntil(x => _fileUploadPage.CheckFileInputVisible());

            string tempFilePath = Path.GetTempFileName();
            using (StreamWriter outputFile = new StreamWriter(tempFilePath, false))
            {
                outputFile.WriteLine("Test123");
            }
            _fileUploadPage.UploadAFile(tempFilePath);

            WaitUntil(x => _fileUploadPage.GetUploadedFilesCount() > 0);

            Assert.AreEqual("Do not forget to delete files from your temporary folder!", _fileUploadPage.InfoLabel.Text);

            _fileUploadPage.DeleteAllFiles();
            WaitUntil(x => _fileUploadPage.GetUploadedFilesCount() == 0);

            Assert.IsTrue(!_fileUploadPage.InfoLabel.Displayed);
        }
        [Test]
        public void FileUploadPage_GitHubSourceURL()
        {
            WaitUntil(x => _fileUploadPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
