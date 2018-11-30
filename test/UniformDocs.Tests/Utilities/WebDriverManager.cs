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
                        capability.SetCapability("browserName", "Chrome");
                        capability.SetCapability("platform", "Windows 10");
                        capability.SetCapability("version", "70.0");
                        break;
                    }
                case Config.Browser.Edge:
                    {
                        capability.SetCapability("browserName", "MicrosoftEdge");
                        capability.SetCapability("platform", "Windows 10");
                        capability.SetCapability("version", "16.16299");
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
                        capability.SetCapability("browserName", "Firefox");
                        capability.SetCapability("platform", "Windows 10");
                        capability.SetCapability("version", "63.0");
                        break;
                    }
                case Config.Browser.iOS12:
                    {                        
                        capability.SetCapability("deviceName", "iPhone XS Simulator");
                        capability.SetCapability("deviceOrientation", "portrait");
                        capability.SetCapability("platformVersion", "12.0");
                        capability.SetCapability("platformName", "iOS");
                        capability.SetCapability("browserName", "Safari");
                        break;
                    }
                case Config.Browser.iOS11point3:
                    {
                        capability.SetCapability("deviceName", "iPhone 8 Simulator");
                        capability.SetCapability("deviceOrientation", "portrait");
                        capability.SetCapability("platformVersion", "11.3");
                        capability.SetCapability("platformName", "iOS");
                        capability.SetCapability("browserName", "Safari");
                        break;
                    }
                case Config.Browser.Android7Nougat:
                    {
                        capability.SetCapability("deviceName", "Android GoogleAPI Emulator");
                        capability.SetCapability("deviceOrientation", "portrait");
                        capability.SetCapability("browserName", "Chrome");
                        capability.SetCapability("platformVersion", "7.0");
                        capability.SetCapability("platformName", "Android");
                        break;
                    }                
                case Config.Browser.Android7point1Nougat:
                    {
                        capability.SetCapability("deviceName", "Android GoogleAPI Emulator");
                        capability.SetCapability("deviceOrientation", "portrait");
                        capability.SetCapability("browserName", "Chrome");
                        capability.SetCapability("platformVersion", "7.1");
                        capability.SetCapability("platformName", "Android");
                        break;
                    }
                case Config.Browser.Android5point1Lolliopop:
                    {
                        capability.SetCapability("deviceName", "Android GoogleAPI Emulator");
                        capability.SetCapability("deviceOrientation", "portrait");
                        capability.SetCapability("browserName", "Browser");
                        capability.SetCapability("platformVersion", "5.1");
                        capability.SetCapability("platformName", "Android");
                        break;
                    }
            }

            if (remoteWebDriverUri == Config.RemoteWebDriverUri)
            {
                IsCloud = true;
                capability.SetCapability("recordVideo", false);
                capability.SetCapability("recordScreenshots", false);
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
                       
            ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (outcome.Equals(ResultState.Success) ? "passed" : "failed"));
        }
    }
}