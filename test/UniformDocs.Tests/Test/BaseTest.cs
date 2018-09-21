using System;
using System.Collections.Generic;
using System.Linq;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Net;

namespace UniformDocs.Tests.Test
{
    public class BaseTest
    {
        public IWebDriver Driver;
        private readonly Config.Browser _browser;
        private readonly string _browsersTc = TestContext.Parameters["Browsers"];
        private List<string> _browsersToRun = new List<string>();
        private ResultState LastOutcome = null;
        private string LastOutcomeMessage = null;

        public BaseTest(Config.Browser browser)
        {
            _browser = browser;
        }

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            if (_browsersTc != null)
            {
                _browsersToRun = _browsersTc.Split(',').ToList();
            }
            else
            {
                _browsersToRun.Add("Chrome");
                _browsersToRun.Add("Firefox");
                //_browsersToRun.Add("Edge");
            }

            Uri serverUri = Config.RemoteWebDriverUri;
            if (TestContext.Parameters["Server"] != null)
            {
                serverUri = new Uri(TestContext.Parameters["Server"]);
            }

            if (_browsersToRun.Contains(Config.BrowserDictionary[_browser]))
            {
                Driver = WebDriverManager.StartDriver(_browser, Config.Timeout, serverUri);
            }
            else
            {
                Assert.Ignore(Config.BrowserDictionary[_browser] + " is on browsers ignore list");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (LastOutcome == null || TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                //this class has single driver session for all test methods
                //we don't want to mark a test as passed if it was already marked as failed
                LastOutcome = TestContext.CurrentContext.Result.Outcome;

                //LastOutcomeMessage = TestContext.CurrentContext.Result.StackTrace;
                LastOutcomeMessage = TestContext.CurrentContext.Result.Message;
                /*
                example stack trace:
                (Marked via REST API: at OpenQA.Selenium.Support.UI.DefaultWait`1.ThrowTimeoutException(String exceptionMessage, Exception lastException) at OpenQA.Selenium.Support.UI.DefaultWait`1.Until[TResult](Func`2 condition) at UniformDocs.Tests.Test.BaseTest.WaitForText(IWeb)

                example message:
                (Marked via REST API: OpenQA.Selenium.WebDriverTimeoutException : Timed out after 5 seconds) 
                */
            }
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            if (WebDriverManager.IsCloud)
            {
                WebDriverManager.MarkTestStatusOnBrowserStack(LastOutcome, LastOutcomeMessage);
            }
            WebDriverManager.StopDriver();
        }

        private string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, string errorMessage = null, int timeToWait = 10)
        {
            int tries = 5;
            Exception lastException = null;
            while(tries-- > 0)
            {
                // sometimes the github-source-element reference refers to the old now-stale page,
                // which throws an excpetion. This gives it 4 more chances
                // this is an alternative for IgnoreExceptionTypes because it doesn't work (see https://github.com/SeleniumHQ/selenium/issues/4240)
                try
                {
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait)) { Message = errorMessage };
                    return wait.Until(condition);
                } catch (Exception exp) { lastException = exp; }
            }
            throw lastException;
        }

        public bool WaitForText(IWebElement elementName, string text, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.TextToBePresentInElement(elementName, text));
        }
        private bool IsURLStatus200(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            return (request.GetResponse() as HttpWebResponse)?.StatusCode == HttpStatusCode.OK;
        }
        public void TestGitHubSourceLinkURLs()
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)Driver;
            string urlsString = (string)jsExecuter.ExecuteScript("return Array.from(document.querySelector('github-source-links').shadowRoot.querySelectorAll('a[href]')).map(el => el.href).join('|')");
            var URLs = urlsString.Split(new char[] { '|' });

            foreach(string URL in URLs) {
                Assert.True(IsURLStatus200(URL));
            }
        }
    }
}