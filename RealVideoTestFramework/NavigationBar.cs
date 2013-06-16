using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebTest.TestUtilities;

namespace RealVideo.WebTest.TestFramework
{
    public class NavigationBar : PageBase
    {
        private IWebElement dailyVideosButton;
        private IWebElement newAndPopularButton;
        private IWebElement channelsButton;
        private IWebElement bookmarksButton;
        private IWebElement facebookButton;
        private IWebElement searchButton;
        
        public NavigationBar(IWebDriver driver) : base(driver) 
        {
            this.dailyVideosButton = this.webDriver.FindElement(By.Id("tab-channel_bestofweb"));
            this.newAndPopularButton = this.webDriver.FindElement(By.Id("tab-channel_popular"));
            this.channelsButton = this.webDriver.FindElement(By.Id("tab-channels"));
            this.bookmarksButton = this.webDriver.FindElement(By.Id("tab-bookmarks"));
            this.facebookButton = this.webDriver.FindElement(By.Id("tab-facebook"));
            this.searchButton = this.webDriver.FindElement(By.Id("tab-search"));
        }

        public void GoToBookmarkPage()
        {
            this.bookmarksButton.Click();
        }

        public void GoToDailyVideoPage()
        {
            this.dailyVideosButton.Click();
        }
    }
}
