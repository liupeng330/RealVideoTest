using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebTest.TestUtilities;

namespace RealVideo.WebTest.TestFramework
{
    public class BookmarkPage : PageBase
    {
        private IWebElement refreshButton
        {
            get
            {
                return this.webDriver.FindElement(By.XPath("//div[@id='bookmarks-content']//a[@class='refresh']"));
            }

        }
        private IWebElement rootElement
        {
            get
            {
                return this.webDriver.FindElement(By.XPath("//div[@id='bookmarks-content']/div[@class='sections']"));
            }
        }

        public BookmarkPage(IWebDriver driver)
            : base(driver)
        {
        }

        public void Reresh()
        {
            this.refreshButton.Click();
        }

        public IEnumerable<VideoCell> GetAllVideos()
        {
            foreach (var parent in rootElement.FindElements(By.TagName("a")))
            {
                yield return new VideoCell(parent, webDriver);
            }
        }

        public VideoDrawer GetVideoDrawer()
        {
            return new VideoDrawer(this.rootElement.FindElement(By.ClassName("drawer")), this.webDriver);
        }
    }
}
