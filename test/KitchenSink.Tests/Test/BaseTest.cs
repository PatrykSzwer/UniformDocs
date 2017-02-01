﻿using System;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Tests.Test
{
    public class BaseTest
    {
        public IWebDriver Driver;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            string args = TestContext.Parameters["Browser"];

            Config.Browser browser = Config.Browser.Chrome;

            switch (args)
            {
                case "Chrome":
                    browser = Config.Browser.Chrome;
                    break;
                case "Firefox":
                    browser = Config.Browser.Firefox;
                    break;
                case "Edge":
                    browser = Config.Browser.Edge;
                    break;
            }

            Driver = WebDriverManager.StartDriver(browser, Config.Timeout, Config.RemoteWebDriverUri);
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
           Driver.Quit();
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            return wait.Until(condition);
        }
    }
}