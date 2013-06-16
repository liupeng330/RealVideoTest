using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace RealVideo.WebTest.TestFramework
{
    public class VideoDrawer
    {
        private IWebElement BookMarkButtonParent
        {
            get
            {
                var allDivs = this.parent.FindElements(By.XPath("//div[@class='buttons']/div"));
                return (from i in allDivs where i.GetAttribute("class") == "bookmark button" select i).Single();
            }
        }

        private IWebElement BookMarkButton
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

        private IWebElement UnBookMarkButton
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
    }
}
