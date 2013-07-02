using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebTest.TestUtilities;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace RealVideo.WebTest.TestFramework
{
    public class BookmarkPage : PageBase
    {
        [FindsBy(How = How.XPath, Using = "//div[@id='bookmarks-content']//a[@class='refresh']")]
        private IWebElement refreshButton;

        [FindsBy(How = How.XPath, Using = "//div[@id='bookmarks-content']/div[@class='sections']")]
        private IWebElement rootElement;

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
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 10));
            wait.Until((d) =>
            {
                return this.PresenceOfElementLocated(By.XPath("//div[@id='bookmarks-content']/div[@class='sections']"));
            });
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
