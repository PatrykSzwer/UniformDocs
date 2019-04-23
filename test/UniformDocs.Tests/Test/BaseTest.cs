using System;
using System.Collections.Generic;
using System.Linq;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Net;

namespace UniformDocs.Tests.Test
{
    public class BaseTest
    {
        public RemoteWebDriver Driver;
        private readonly Config.Browser _browser;
        private readonly string _browsersTc = TestContext.Parameters["Browsers"];
        private List<string> _browsersToRun = new List<string>();
        private ResultState LastOutcome;
        private string LastOutcomeMessage;
        protected static readonly string _testedAppName = "UniformDocs";
        private static int failsCount = 0;

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
                _browsersToRun.Add("ChromeNoV0");
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

        [SetUp]
        public virtual void SetUp()
        {
            if (failsCount >= Config.FailsBeforeStop)
            {
                Assert.Inconclusive("The maximum number of test errors was reached. Tests will be stopped.");
            }

            if (!(RestApiHelper.CheckAppRunning(_testedAppName).Result))
            {
                Assert.Fail($"The tested app {_testedAppName} is not running");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (!(RestApiHelper.CheckAppRunning(_testedAppName).Result))
            {
                Assert.Fail($"The tested app {_testedAppName} unexpectedly stopped during the test. " +
                            $"The following text is the last message that can be found in the Starcounter log: " +
                            $"{RestApiHelper.GetLatestLogEntry().Result}");
            }

            if (TestContext.CurrentContext.Result.Outcome == ResultState.Error)
            {
                failsCount++;
            }

            if (LastOutcome == null || TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                //this class has single driver session for all test methods
                //we don't want to mark a test as passed if it was already marked as failed
                LastOutcome = TestContext.CurrentContext.Result.Outcome;
                LastOutcomeMessage = TestContext.CurrentContext.Result.Message;
            }
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Skipped)
            {
                if (WebDriverManager.IsCloud)
                {
                    WebDriverManager.MarkTestStatusOnBrowserStack(Driver, LastOutcome, LastOutcomeMessage);
                }
                WebDriverManager.StopDriver(Driver);
            }
        }

        private string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, string errorMessage = null, int timeToWait = 10)
        {
            int tries = 5;
            Exception lastException = null;
            while (tries-- > 0)
            {
                // sometimes the github-source-element reference refers to the old now-stale page,
                // which throws an excpetion. This gives it 4 more chances
                // this is an alternative for IgnoreExceptionTypes because it doesn't work (see https://github.com/SeleniumHQ/selenium/issues/4240)
                try
                {
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait)) { Message = errorMessage };
                    return wait.Until(condition);
                }
                catch (Exception exp) { lastException = exp; }
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

            foreach (string URL in URLs)
            {
                Assert.True(IsURLStatus200(URL));
            }
        }
    }
}