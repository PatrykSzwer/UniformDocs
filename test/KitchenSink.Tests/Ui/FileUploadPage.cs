using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace KitchenSink.Tests.Ui
{
    public class FileUploadPage : BasePage
    {
        public FileUploadPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".alert-warning")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@slot = 'kitchensink/fileupload-files-table']/descendant::tr[2]")]
        public IList<IWebElement> UploadedFilesList { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@slot = 'kitchensink/fileupload-files-table']/descendant::button")]
        public IList<IWebElement> DeleteButtons { get; set; }

        public void UploadAFile(string filePath)
        {
            GetFileElement().SendKeys(filePath);
        }

        public int GetUploadedFilesCount()
        {
            return UploadedFilesList.Count;
        }

        public bool CheckFileInputVisible()
        {
            return GetFileElement().Enabled;
        }

        public void DeleteAllFiles()
        {
            foreach (var deleteButton in DeleteButtons)
            {
                ClickOn(deleteButton);
            }
        }

        private IWebElement GetFileElement()
        {
            return GetShadowElementByQuerySelector(By.XPath("//starcounter-upload"), "#fileElement");
        }

    }
}
