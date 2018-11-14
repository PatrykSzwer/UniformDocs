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
            iOS11point4,
            iOS11point2,
            iOS11point3,
            Android8Oreo,
            Android7Nougat,
            Android6Marshmello,
            Android8point1Oreo,
            Android7point1Nougat,
            Android5point1Lolliopop
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
        public static readonly Uri RemoteWebDriverUri = new Uri("http://hub-cloud.browserstack.com/wd/hub/");
        public static readonly string BrowserstackUsername = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
        public static readonly string BrowserstackAccessKey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
        public static readonly string BrowserstackLocalIdentifier = Environment.GetEnvironmentVariable("COMPUTERNAME");

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
            {Browser.iOS11point4, "iPhone6S"},
            {Browser.iOS11point2, "iOS11point2"},
            {Browser.iOS11point3, "iOS11point3"},
            {Browser.Android8Oreo, "Andriod8Oreo"},
            {Browser.Android7Nougat, "GoogleNexus9"},
            {Browser.Android7Nougat, "Android7Nougat"},
            {Browser.Android6Marshmello,"Android6Marshmello"},
            {Browser.Android8point1Oreo, "Android8point1Oreo"},
            {Browser.Android7point1Nougat,"Android5point1Lolliopop"},
            {Browser.Android5point1Lolliopop, "Android5point1Lolliopop"}
        };
    }
}