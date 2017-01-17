﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace KitchenSink.Test
{
    public class DatepickerPage : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//pikaday-decorator//input[@class = 'form-control']")]
        public IWebElement datePicker { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class = 'pika-lendar']//table//tbody//td[@class = 'is-selected']//button[@class = 'pika-button pika-day']")]
        public IWebElement selectedDay { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@slot = '4']")]
        public IWebElement yearInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@slot = '6']")]
        public IWebElement monthInput { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@slot = '8']")]
        public IWebElement dayInput { get; set; }

        public DatepickerPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        public void SelectDate(string date)
        {
            datePicker.Clear();
            datePicker.SendKeys(date);
            datePicker.SendKeys(Keys.Enter);
            ClickOn(selectedDay);
        }

        public string GetYear()
        {
            var year = yearInput.GetAttribute("value");
            return year;
        }

        public string GetMonth()
        {
            var month = monthInput.GetAttribute("value");
            return month;
        }

        public string GetDay()
        {
            var day = dayInput.GetAttribute("value");
            return day;
        }
    }
}
