﻿using System;
using System.Diagnostics;
using KitchenSink.Tests.Ui;
using KitchenSink.Tests.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KitchenSink.Tests.Test
{
    [Parallelizable(ParallelScope.Fixtures)]
    [TestFixture(Config.Browser.Chrome)]
    [TestFixture(Config.Browser.Edge)]
    [TestFixture(Config.Browser.Firefox)]
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
            WaitUntil(x => _datePicker.YearInput.Displayed);

            // _datePicker.DateInput.Clear();
            _datePicker.DateInput.SendKeys("01/01/2016");
            _datePicker.DateInput.SendKeys(Keys.Enter);

            WaitUntil(x => _datePicker.YearInput.GetAttribute("value") == "2016", $"Expected: 2016, but was: {_datePicker.YearInput.GetAttribute("value")}");
            StringAssert.AreEqualIgnoringCase("January", _datePicker.MonthInput.GetAttribute("value"));
            StringAssert.AreEqualIgnoringCase("1", _datePicker.DayInput.GetAttribute("value"));
        }
        // TODO: write tests that clicks through uni-date-picker -> vaadin-date-picker
    }
}
