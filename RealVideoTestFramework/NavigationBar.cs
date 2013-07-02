using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebTest.TestUtilities;
using OpenQA.Selenium.Support.PageObjects;

namespace RealVideo.WebTest.TestFramework
{
    public class NavigationBar : PageBase
    {
        [FindsBy(How = How.Id, Using = "tab-channel_bestofweb")]
        private IWebElement dailyVideosButton;

        [FindsBy(How = How.Id, Using = "tab-channel_popular")]
        private IWebElement newAndPopularButton;

        [FindsBy(How = How.Id, Using = "tab-channels")]
        private IWebElement channelsButton;

        [FindsBy(How = How.Id, Using = "tab-bookmarks")]
        private IWebElement bookmarksButton;

        [FindsBy(How = How.Id, Using = "tab-facebook")]
        private IWebElement facebookButton;

        [FindsBy(How = How.Id, Using = "tab-search")]
        private IWebElement searchButton;
        
        public NavigationBar(IWebDriver driver) : base(driver) 
        {
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
