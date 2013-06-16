using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace RealVideo.WebTest.TestFramework
{
    public class VideoCell
    {
        private IWebElement PlayButton;
        private IWebElement BookMarkButton;
        private IWebElement Image;
        private IWebElement CellTitle;

        private IWebDriver driver;
        private IWebElement parent;

        public string ImageSrc { get; private set; }
        public string Title { get; private set; }

        public VideoCell(IWebElement parent, IWebDriver driver)
        {
            this.driver = driver;
            this.parent = parent;
            this.PlayButton = parent.FindElement(By.ClassName("play-overlay"));
            this.BookMarkButton = parent.FindElement(By.ClassName("bookmark-indicator"));

            this.Image = parent.FindElement(By.ClassName("thumb")).FindElement(By.TagName("img"));
            this.ImageSrc = this.Image.GetAttribute("src");

            this.CellTitle = parent.FindElement(By.ClassName("title"));
            this.Title = this.CellTitle.Text;
        }

        public void Play()
        {
            this.Image.Click();
        }

        public void Bookmark()
        {
            Actions action = new Actions(this.driver);
            action.MoveToElement(this.Image).MoveToElement(this.BookMarkButton).Click().Perform();
        }
    }
}
