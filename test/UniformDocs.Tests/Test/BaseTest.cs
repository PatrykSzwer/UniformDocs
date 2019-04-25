using System;
using System.Collections.Generic;
using System.Linq;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace UniformDocs.Tests.Test
{
    public class BaseTest
    {
        public RemoteWebDriver Driver;
        private readonly Config.Browser _browser;
        private readonly string _browsersTc = TestContext.Parameters["Browsers"];
        private List<string> _browsersToRun = new List<string>();
        private ResultState _lastOutcome;
        private string _lastOutcomeMessage;

        public BaseTest(Config.Browser browser)
        {
            _browser = browser;
        }

        [OneTimeSetUp]
        public async Task TestFixtureSetUp()
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
                var tries = 0;

                while (tries <= 5)
                {
                    try
                    {
                        Driver = WebDriverManager.StartDriver(_browser, Config.Timeout, serverUri);
                        break;
                    }
                    catch (WebDriverException ex)
                    {
                        if (ex.Message.Contains("All parallel tests are currently in use"))
                        {
                            await Task.Delay(TimeSpan.FromSeconds(60));
                            tries++;
                        }
                    }

                    if (Driver != null)
                    {
                        break;
                    }
                }

                if (Driver == null)
                {
                    throw new Exception("Can't create WebDriver, probably due to a bad config or because all parallel tests are currently in use.");
                }
            }
            else
            {
                Assert.Ignore(Config.BrowserDictionary[_browser] + " is on browsers ignore list");
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (_lastOutcome == null || TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                //this class has single driver session for all test methods
                //we don't want to mark a test as passed if it was already marked as failed
                _lastOutcome = TestContext.CurrentContext.Result.Outcome;
                _lastOutcomeMessage = TestContext.CurrentContext.Result.Message;
            }
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Skipped && 
                TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed &&
                Driver != null)
            {
                if (WebDriverManager.IsCloud)
                {
                    WebDriverManager.MarkTestStatusOnBrowserStack(Driver, _lastOutcome, _lastOutcomeMessage);
                }
                WebDriverManager.StopDriver(Driver);
            }
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