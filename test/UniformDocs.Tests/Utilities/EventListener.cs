using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace UniformDocs.Tests.Utilities
{
    class UniformDocsTestEventListener : EventFiringWebDriver
    {
        public UniformDocsTestEventListener(IWebDriver driver) : base(driver)
        {
            ExceptionThrown += (sender, e) =>
            {
                if (e != null && sender != null)
                {
                    //Screenshot.MakeScreenshot(e.Driver);
                }
            };
        }
    }
}