using System;
using System.Diagnostics;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace UniformDocs.Tests.Test
{
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
    [TestFixture(Config.Browser.iOS11point2)]
    [TestFixture(Config.Browser.Android7Nougat)]
    [TestFixture(Config.Browser.Android8Oreo)]
    [TestFixture(Config.Browser.iOS11point4)]
    [TestFixture(Config.Browser.iOS12)]
    [TestFixture(Config.Browser.iOS11point3)]
    [TestFixture(Config.Browser.Android7Nougat)]
    [TestFixture(Config.Browser.Android6Marshmello)]
    [TestFixture(Config.Browser.Android8point1Oreo)]
    [TestFixture(Config.Browser.Android7point1Nougat)]
    [TestFixture(Config.Browser.Android5point1Lolliopop)]
    class DatepickerPageTest : BaseTest
    {
        private DatepickerPage _datePicker;
        private MainPage _mainPage;

        public DatepickerPageTest(Config.Browser browser) : base(browser)
        {
        }

        [SetUp]
        public void SetUp()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _datePicker = _mainPage.GoToDatePickerPage();
        }

        [Test]
        public void DatepickerPage_SelectDate()
        {
            WaitUntil(x => _datePicker.IsLoaded(), $"Expected date picker to be fully loaded");

            _datePicker.SelectThroughUniDatePicker("2016-01-01");

            WaitUntil(x => _datePicker.YearInput.GetAttribute("value") == "2016", $"Expected: 2016, but was: {_datePicker.YearInput.GetAttribute("value")}");
            StringAssert.AreEqualIgnoringCase("January", _datePicker.MonthInput.GetAttribute("value"));
            StringAssert.AreEqualIgnoringCase("1", _datePicker.DayInput.GetAttribute("value"));
        }
        [Test]
        public void DatepickerPage_GitHubSourceURL()
        {
            WaitUntil(x => _datePicker.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
