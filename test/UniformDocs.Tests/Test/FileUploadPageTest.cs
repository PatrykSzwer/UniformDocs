using UniformDocs.Tests.Ui;
using NUnit.Framework;
using System.IO;
using UniformDocs.Tests.Utilities;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private FileUploadPage _fileUploadPage;
 
        public void InitFileUploadPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _fileUploadPage = _mainPage.GoToFileUploadPage();
        }

        [Test]
        public void FileUploadPage_UploadAFile()
        {
            InitFileUploadPageTest();
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
            InitFileUploadPageTest();
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
            InitFileUploadPageTest();
            WaitUntil(x => _fileUploadPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
