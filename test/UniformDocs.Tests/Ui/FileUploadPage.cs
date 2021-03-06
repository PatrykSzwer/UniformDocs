﻿using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class FileUploadPage : BasePage
    {
        public FileUploadPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/fileupload-files-table-message']")]
        public IWebElement InfoLabel { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/fileupload-files-table'] tbody tr")]
        public IList<IWebElement> UploadedFilesList { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[slot = 'uniformdocs/fileupload-files-table'] button")]
        public IList<IWebElement> DeleteButtons { get; set; }

        public void UploadAFile(string filePath)
        {
            var inputFileElement = GetFileElement();
            var script = "return arguments[0].classList.contains('style-scope') ? arguments[0].style.display = 'initial' : true;"; //in Shadow DOM polyfill (Firefox), Selenium cannot type to an input that is "display: none" and throws ElementNotInteractableException. This line fixes the problem
            ExecuteScriptOnElement(inputFileElement, script);
            inputFileElement.SendKeys(filePath);
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
