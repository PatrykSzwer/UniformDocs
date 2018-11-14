using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using System.Text;
using System.Net;
using System.IO;
using NUnit.Framework.Interfaces;
using Newtonsoft.Json.Linq;

namespace UniformDocs.Tests.Utilities
{
    public class WebDriverManager
    {
        public static bool IsCloud;

        public static RemoteWebDriver StartDriver(Config.Browser browser, TimeSpan timeout, Uri remoteWebDriverUri)
        {
            DesiredCapabilities capability = new DesiredCapabilities();

            switch (browser)
            {
                case Config.Browser.Chrome:
                    {
                        capability.SetCapability("os", "Windows");
                        capability.SetCapability("os_version", "10");
                        capability.SetCapability("browser", "Chrome");
                        capability.SetCapability("browser_version", "69.0");
                        break;
                    }
                case Config.Browser.Edge:
                    {
                        capability.SetCapability("os", "Windows");
                        capability.SetCapability("os_version", "10");
                        capability.SetCapability("browser", "Edge");
                        capability.SetCapability("browser_version", "17.0");
                        break;
                    }
                case Config.Browser.Firefox:
                    {
                        var firefoxOptions = new FirefoxOptions();
                        firefoxOptions.SetPreference("browser.download.folderList", 0); //0 is recommended per BrowserStack docs; https://www.browserstack.com/automate/c-sharp#enhancements-uploads-downloads
                        firefoxOptions.SetPreference("browser.download.manager.focusWhenStarting", false);
                        firefoxOptions.SetPreference("browser.download.useDownloadDir", true);
                        firefoxOptions.SetPreference("browser.helperApps.alwaysAsk.force", false);
                        firefoxOptions.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                        firefoxOptions.SetPreference("browser.download.manager.closeWhenDone", true);
                        firefoxOptions.SetPreference("browser.download.manager.showAlertOnComplete", false);
                        firefoxOptions.SetPreference("browser.download.manager.useWindow", false);
                        firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "image/svg+xml;application/force-download");
                        capability = (DesiredCapabilities)firefoxOptions.ToCapabilities();
                        capability.SetCapability("os", "Windows");
                        capability.SetCapability("os_version", "10");
                        capability.SetCapability("browser", "Firefox");
                        capability.SetCapability("browser_version", "62.0");
                        break;
                    }
                case Config.Browser.iOS12:
                    {
                        capability.SetCapability("os_version", "12.0");
                        capability.SetCapability("device", "iPhone XS");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.iOS11point4:
                    {
                        capability.SetCapability("os_version", "11.4");
                        capability.SetCapability("device", "iPhone 6S");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.iOS11point2:
                    {
                        capability.SetCapability("os_version", "11.2");
                        capability.SetCapability("device", "iPhone 6");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.iOS11point3:
                    {
                        capability.SetCapability("os_version", "11.3");
                        capability.SetCapability("device", "iPad 6th");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.Android8Oreo:
                    {
                        capability.SetCapability("os_version", "8.0");
                        capability.SetCapability("device", "Samsung Galaxy S9");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.Android8point1Oreo:
                    {
                        capability.SetCapability("os_version", "8.1");
                        capability.SetCapability("device", "Samsung Galaxy Note 9");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.Android7Nougat:
                    {
                        capability.SetCapability("os_version", "7.0");
                        capability.SetCapability("device", "Samsung Galaxy S8");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }                
                case Config.Browser.Android7point1Nougat:
                    {
                        capability.SetCapability("os_version", "7.1");
                        capability.SetCapability("device", "Google Pixel");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
                case Config.Browser.Android5point1Lolliopop:
                    {
                        capability.SetCapability("os_version", "5.1");
                        capability.SetCapability("device", "Google Nexus 9");
                        capability.SetCapability("real_mobile", "true");
                        break;
                    }
            }

            if (remoteWebDriverUri == Config.RemoteWebDriverUri)
            {
                IsCloud = true;
                capability.SetCapability("browserstack.user", Config.BrowserstackUsername);
                capability.SetCapability("browserstack.key", Config.BrowserstackAccessKey);
                capability.SetCapability("browserstack.debug", "true");
                capability.SetCapability("browserstack.local", "true");
                capability.SetCapability("browserstack.localIdentifier", Config.BrowserstackLocalIdentifier);
                capability.SetCapability("browserstack.video", "false"); //video recording increases test time, see https://www.browserstack.com/automate/c-sharp
                capability.SetCapability("browserstack.use_w3c", "true"); //needed for closing of window in UrlPage_ClickBlankTargettedLink for Firefox on BrowserStack; see https://github.com/SeleniumHQ/selenium/issues/5064
                capability.SetCapability("browserstack.console", "verbose");
            }

            capability.SetCapability("project", "UniformDocs");
            capability.SetCapability("name", NUnit.Framework.TestContext.CurrentContext.Test.FullName);

            var driver = new RemoteWebDriver(remoteWebDriverUri, capability);

            var allowsDetection = driver as IAllowsFileDetection;
            if (allowsDetection != null)
            {
                allowsDetection.FileDetector = new LocalFileDetector();
            }

            driver.Manage().Timeouts().PageLoad = timeout;
            driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
            return driver;
        }

        public static void StopDriver(RemoteWebDriver driver)
        {
            driver.Quit();
        }
        public static void MarkTestStatusOnBrowserStack(RemoteWebDriver driver, ResultState outcome, string message)
        {
            if (IsCloud == false)
            {
                throw new Exception("You are not testing on the cloud");
            }

            string status = "failed";

            if (outcome.Equals(ResultState.Success))
            {
                status = "passed";
            }

            JObject json = new JObject();
            json["status"] = status;
            json["reason"] = message;
            string reqString = json.ToString();

            SessionId sessionId = driver.SessionId;

            byte[] requestData = Encoding.UTF8.GetBytes(reqString);
            Uri myUri = new Uri($"https://www.browserstack.com/automate/sessions/{sessionId.ToString()}.json");
            WebRequest myWebRequest = HttpWebRequest.Create(myUri);
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myWebRequest.ContentType = "application/json";
            myWebRequest.Method = "PUT";
            myWebRequest.ContentLength = requestData.Length;
            using (Stream st = myWebRequest.GetRequestStream()) st.Write(requestData, 0, requestData.Length);

            NetworkCredential myNetworkCredential = new NetworkCredential(Config.BrowserstackUsername, Config.BrowserstackAccessKey);
            CredentialCache myCredentialCache = new CredentialCache();
            myCredentialCache.Add(myUri, "Basic", myNetworkCredential);
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Credentials = myCredentialCache;

            myWebRequest.GetResponse().Close();
        }
    }
}