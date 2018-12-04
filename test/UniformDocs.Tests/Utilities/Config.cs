using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace UniformDocs.Tests.Utilities
{
    public class Config
    {
        public enum Browser
        {
            Chrome,
            Edge,
            Firefox,
            iOS12,
            iOS11_2,
            iOS11_3,
            Android7,
            Android6,
            Android7_1,
            Android5_1
        }

        public enum Buttons
        {
            Bread,
            Vegetable,
            Fruit,
            Morph,
            Redirect
        }

        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(60);
        public static readonly string LocalIP = Dns.GetHostEntry(Dns.GetHostName())
   .AddressList.First(
       f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
   .ToString();
        public static readonly Uri TestedAppUrl = new Uri($"http://{LocalIP}:8080/UniformDocs");
        public static readonly string SauceLabsUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME");
        public static readonly string SauceLabsAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY");
        public static readonly Uri RemoteWebDriverUri = new Uri($"http://ondemand.saucelabs.com:80/wd/hub");

        public static readonly Dictionary<Buttons, string> ButtonsDictionary = new Dictionary<Buttons, string>
        {
            {Buttons.Bread, "Bread"},
            {Buttons.Vegetable, "Vegetable"},
            {Buttons.Fruit, "Fruit"},
            {Buttons.Morph, "Morph"},
            {Buttons.Redirect, "Redirect"}
        };

        public static readonly Dictionary<Browser, string> BrowserDictionary = new Dictionary<Browser, string>
        {
            {Browser.Chrome, "Chrome"},
            {Browser.Edge, "Edge"},
            {Browser.Firefox, "Firefox"},
            {Browser.iOS12, "iOS12"},
            {Browser.iOS11_2, "iOS11_2"},
            {Browser.iOS11_3, "iOS11_3"},
            {Browser.Android7, "Android7"},
            {Browser.Android6,"Android6"},
            {Browser.Android7_1,"Android7_1"},
            {Browser.Android5_1, "Android5_1"}
        };
    }
}