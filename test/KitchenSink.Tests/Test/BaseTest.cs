using System;
using System.Collections.Generic;
using System.Linq;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework.Interfaces;
using System.IO;

namespace KitchenSink.Tests.Test
{
    public class BaseTest
    {
        public IWebDriver Driver;
        private readonly Config.Browser _browser;
        private readonly string _browsersTc = TestContext.Parameters["Browsers"];
        private List<string> _browsersToRun = new List<string>();

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

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success && Driver != null)
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var dirPath = "C:\\selenium";

                if (!Directory.Exists(dirPath))
                {
                    throw new Exception($"I cannot make a screenshot of the failed test because the directory {dirPath} does not exist");
                }

                string filePath = $"{dirPath}\\Test fail {GetSafeFilename(TestContext.CurrentContext.Test.FullName)}.png";
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            }
            Driver?.Quit();
        }

        private string GetSafeFilename(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, string errorMessage = null)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)) { Message = errorMessage };
            return wait.Until(condition);
        }

        public bool WaitForText(IWebElement elementName, string text, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.TextToBePresentInElement(elementName, text));
        }
    }
}