using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace UniformDocs.Tests.Utilities
{
    public class WebDriverManager
    {
        private static IWebDriver Driver;

        public static IWebDriver StartDriver(Config.Browser browser, TimeSpan timeout, Uri remoteWebDriverUri)
        {
            DesiredCapabilities capability = null;

            switch (browser)
            {
                case Config.Browser.Chrome:
                    {
                        capability = new DesiredCapabilities();
                        capability.SetCapability("os", "Windows");
                        capability.SetCapability("os_version", "10");
                        capability.SetCapability("browser", "Chrome");
                        capability.SetCapability("browser_version", "69.0");
                        break;
                    }
                case Config.Browser.Edge:
                    {
                        capability = new DesiredCapabilities();
                        capability.SetCapability("os", "Windows");
                        capability.SetCapability("os_version", "10");
                        capability.SetCapability("browser", "Edge");
                        capability.SetCapability("browser_version", "17.0");
                        break;
                    }
                case Config.Browser.Firefox:
                    {
                        var firefoxOptions = new FirefoxOptions();
                        firefoxOptions.SetPreference("browser.download.folderList", 2);
                        firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "image/svg+xml");
                        capability = (DesiredCapabilities)firefoxOptions.ToCapabilities();
                        capability.SetCapability("os", "Windows");
                        capability.SetCapability("os_version", "10");
                        capability.SetCapability("browser", "Firefox");
                        capability.SetCapability("browser_version", "62.0");
                        break;
                    }
            }

            if (remoteWebDriverUri == Config.RemoteWebDriverUri)
            {
                capability.SetCapability("browserstack.user", Config.BrowserstackUsername);
                capability.SetCapability("browserstack.key", Config.BrowserstackAccessKey);
                capability.SetCapability("browserstack.debug", "true");
                capability.SetCapability("browserstack.local", "true");
                capability.SetCapability("browserstack.localIdentifier", Config.BrowserstackLocalIdentifier);
            }

            capability.SetCapability("project", "UniformDocs");
            capability.SetCapability("name", NUnit.Framework.TestContext.CurrentContext.Test.FullName);
            Driver = new RemoteWebDriver(remoteWebDriverUri, capability);

            IWebDriver eventDriver = new UniformDocsTestEventListener(Driver);
            Driver = eventDriver;
            Driver.Manage().Timeouts().PageLoad = timeout;
            Driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
            return Driver;
        }

        public static void StopDriver()
        {
            Driver?.Quit();
        }
    }
}