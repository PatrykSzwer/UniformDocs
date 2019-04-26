using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using OpenQA.Selenium.Support.UI;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private UrlPage _urlPage;

        public void InitUrlPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _urlPage = _mainPage.GoToUrlPage();
        }

        private string GetIframeCurrentURL()
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)Driver;
            return (string)jsExecuter.ExecuteScript("return document.querySelector('#link-target').contentWindow.location.href");
        }


        [Test]
        public void UrlPage_ClickSimpleLink()
        {
            InitUrlPageTest();
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)Driver;
            WaitUntil(x => _urlPage.SimpleMorphableLink.Displayed);

            // leave a foot print in the window object
            jsExecuter.ExecuteScript("window.footprintExists = true");

            // control test
            Assert.AreEqual($"{Config.TestedAppUrl}/Url", Driver.Url);
            Assert.AreEqual(true, jsExecuter.ExecuteScript("return window.footprintExists"));

            _urlPage.ClickSimpleMorphableLink();

            System.Threading.Thread.Sleep(2000);

            Assert.AreEqual(Driver.Url, Config.TestedAppUrl.ToString());

            // if the foot print still exists, we can infer that the page was actually morphed, not fully loaded
            Assert.AreEqual(true, jsExecuter.ExecuteScript("return window.footprintExists"));

        }

        [Test]
        public void UrlPage_ClickBlankTargettedLink()
        {
            InitUrlPageTest();
            WaitUntil(x => _urlPage.BlankTargettedLink.Displayed);

            //control test
            Assert.AreEqual(Driver.WindowHandles.Count, 1);

            WaitUntil(x => ExpectedConditions.ElementToBeClickable(_urlPage.BlankTargettedLink));
            _urlPage.ClickBlankTargettedLink();

            System.Threading.Thread.Sleep(500);

            Assert.AreEqual(Driver.WindowHandles.Count, 2);

            //close pop up
            Driver.SwitchTo().Window(Driver.WindowHandles[1]);
            Driver.Close();

            // use the original page
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);
        }

        [Test]
        public void UrlPage_ClickLinkWithDownloadAttribute()
        {
            InitUrlPageTest();
            WaitUntil(x => _urlPage.LinkWithDownloadAttrib.Displayed);

            _urlPage.ClickLinkWithDownloadAttrib();

            WaitUntil(x => _urlPage.DownloadLinkFeedback.Text == "Your download has started");
        }

        [Test]
        public void UrlPage_ClickIframeTargettedLink()
        {
            InitUrlPageTest();
            WaitUntil(x => _urlPage.IframeTargettedLink != null && _urlPage.IframeTargettedLink.Displayed);

            //control test
            Assert.AreEqual(GetIframeCurrentURL(), "about:blank");

            _urlPage.ClickIframeTargettedLink();

            System.Threading.Thread.Sleep(500);

            Assert.AreEqual(GetIframeCurrentURL(), Config.TestedAppUrl.ToString());
        }
        [Test]
        public void UrlPage_GitHubSourceURL()
        {
            InitUrlPageTest();
            WaitUntil(x => _urlPage.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
