using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace RealVideo.WebTest.TestFramework
{
    public class VideoDrawer
    {
        private WebDriverWait wait;

        private IWebElement BookMarkButtonParent
        {
            get
            {
                var allDivs = this.parent.FindElements(By.XPath("//div[@class='buttons']/div"));
                return (from i in allDivs where i.GetAttribute("class") == "bookmark button" select i).Single();
            }
        }

        public IWebElement BookMarkButton
        {
            get
            {
                return BookMarkButtonParent.FindElement(By.TagName("a"));
            }
        }

        private IWebElement UnBookMarkButtonParent
        {
            get
            {
                var allDivs = this.parent.FindElements(By.XPath("//div[@class='buttons']/div"));
                return (from i in allDivs where i.GetAttribute("class") == "unbookmark button" select i).Single();
            }
        }

        public IWebElement UnBookMarkButton
        {
            get
            {
                return UnBookMarkButtonParent.FindElement(By.TagName("a"));
            }
        }

        private IWebElement Title
        {
            get
            {
                return this.parent.FindElement(By.ClassName("title"));
            }
        }

        private IWebElement Description
        {
            get
            {
                return this.parent.FindElement(By.ClassName("description"));
            }
        }

        private IWebDriver driver;
        private IWebElement parent;

        public VideoDrawer(IWebElement parent, IWebDriver driver)
        {
            this.parent = parent;
            this.driver = driver;
            this.wait = new WebDriverWait(driver, new TimeSpan(0, 0, 1));
        }

        public void BookMark()
        {
            this.BookMarkButton.Click();
        }

        public void UnBookMark()
        {
            this.UnBookMarkButton.Click();
        }

        public bool IsBookMarked
        {
            get
            {
                string styleOfBookmarkButton = this.BookMarkButtonParent.GetAttribute("style");
                string styleOfUnBookmarkButton = this.UnBookMarkButtonParent.GetAttribute("style");
                return styleOfBookmarkButton.Contains("none") && styleOfUnBookmarkButton.Contains("block");
            }
        }

        public void WaitForChangeToUnBookMark()
        {
            wait.Until((d) => { return IsBookMarked; });
        }

        public void WaitForChangeToBookMark()
        {
            wait.Until((d) => { return !IsBookMarked; });
        }
    }
}
