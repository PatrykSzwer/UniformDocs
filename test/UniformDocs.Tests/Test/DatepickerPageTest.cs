using System;
using System.Diagnostics;
using UniformDocs.Tests.Ui;
using UniformDocs.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace UniformDocs.Tests.Test
{
    partial class BaseTest

    {
        private DatepickerPage _datePicker;

        public void InitDatepickerPageTest()
        {
            _mainPage = new MainPage(Driver).GoToMainPage();
            _datePicker = _mainPage.GoToDatePickerPage();
        }

        [Test]
        public void DatepickerPage_SelectDate()
        {
            InitDatepickerPageTest();
            WaitUntil(x => _datePicker.IsLoaded(), $"Expected date picker to be fully loaded");

            _datePicker.SelectThroughUniDatePicker("2016-01-01");

            WaitUntil(x => _datePicker.YearInput.GetAttribute("value") == "2016", $"Expected: 2016, but was: {_datePicker.YearInput.GetAttribute("value")}");
            StringAssert.AreEqualIgnoringCase("January", _datePicker.MonthInput.GetAttribute("value"));
            StringAssert.AreEqualIgnoringCase("1", _datePicker.DayInput.GetAttribute("value"));
        }
        [Test]
        public void DatepickerPage_GitHubSourceURL()
        {
            InitDatepickerPageTest();
            WaitUntil(x => _datePicker.GitHubSourceLinks.Displayed);
            TestGitHubSourceLinkURLs();
        }
    }
}
