using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace UniformDocs.Tests.Utilities
{
    public class WebDriverManager
    {
        public static IWebDriver StartDriver(Config.Browser browser, TimeSpan timeout, Uri remoteWebDriverUri)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Config.Browser.Chrome:
                    {
                        driver = new RemoteWebDriver(remoteWebDriverUri, new ChromeOptions());
                        break;
                    }
                case Config.Browser.Edge:
                    {
                        driver = new RemoteWebDriver(remoteWebDriverUri, new EdgeOptions());
                        break;
                    }
                case Config.Browser.Firefox:
                    {
                        FirefoxOptions profile = new FirefoxOptions();
                        profile.SetPreference("browser.download.folderList", 2);
                        profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "image/svg+xml");

                        driver = new RemoteWebDriver(remoteWebDriverUri, profile);

                        break;
                    }
            }

            IWebDriver eventDriver = new UniformDocsTestEventListener(driver);
            driver = eventDriver;
            driver.Manage().Timeouts().PageLoad = timeout;
            driver.Manage().Timeouts().AsynchronousJavaScript = timeout;
            return driver;
        }
    }
}