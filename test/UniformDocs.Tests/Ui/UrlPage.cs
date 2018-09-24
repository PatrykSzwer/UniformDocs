using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UniformDocs.Tests.Ui
{
    public class UrlPage : BasePage
    {
        public UrlPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Morphable link: This a sample link']")]
        public IWebElement SimpleMorphableLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Non-morphable link: This a sample link']")]
        public IWebElement BlankTargettedLink { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Non-morphable downloadable link: SVG icon']")]
        public IWebElement LinkWithDownloadAttrib { get; set; }

        [FindsBy(How = How.CssSelector, Using = "output[slot='uniformdocs/download-attrib-link-feedback']")]
        public IWebElement DownloadLinkFeedback { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text() = 'Non-morphable link that opens in a frame: This a sample link']")]
        public IWebElement IframeTargettedLink { get; set; }

        public void ClickSimpleMorphableLink()
        {
            ClickOn(SimpleMorphableLink);
        }
        public void ClickBlankTargettedLink()
        {
            ClickOn(BlankTargettedLink);
        }
        public void ClickIframeTargettedLink()
        {
            ClickOn(IframeTargettedLink);
        }
        public void ClickLinkWithDownloadAttrib()
        {
            ClickOn(LinkWithDownloadAttrib);
        }


    }
}
