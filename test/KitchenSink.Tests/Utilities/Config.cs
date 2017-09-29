﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace KitchenSink.Tests.Utilities
{
    public class Config
    {
        public enum Browser
        {
            Chrome,
            Edge,
            Firefox
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
        public static readonly Uri KitchenSinkUrl = new Uri($"http://{LocalIP}:8080/KitchenSink");
        public static readonly Uri RemoteWebDriverUri = new Uri("http://localhost:4444/wd/hub");

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
            {Browser.Firefox, "Firefox"}
        };
    }
}