using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using WebTest.TestUtilities;
using OpenQA.Selenium.Support.PageObjects;

namespace RealVideo.WebTest.TestFramework
{
    public class DailyVidoesPage : PageBase
    {
        [FindsBy(How = How.XPath, Using = "//div[@id='daily-videos-content']/div[@class='sections']")]
        private IWebElement rootElement;

        [FindsBy(How = How.XPath, Using = "//div[@id='daily-videos-content']/div[@class='sections']/div[@class='featured section']")]
        private IWebElement todayTop5Section;

        public DailyVidoesPage(IWebDriver driver) : base(driver) { }

        public IEnumerable<VideoCell> GetTodayTopFiveVideos()
        {
            var allPositions = todayTop5Section.FindElement(By.ClassName("row")).FindElements(By.TagName("a"));
            foreach (var i in allPositions)
            {
                yield return new VideoCell(i, this.webDriver);
            }
        }

        public IEnumerable<int> GetTodayTopBookmarkedVideosIndex()
        {
            var allPositions = todayTop5Section.FindElement(By.ClassName("row")).FindElements(By.TagName("a"));
            List<int> ret = new List<int>();
            for (int i = 0; i < allPositions.Count; i++)
            {
                if (allPositions[i].GetAttribute("class").Contains("bookmarked"))
                {
                    ret.Add(i);
                }
            }
            return ret;
        }

        public IEnumerable<VideoCell> GetLastDaysVideos(int last)
        {
            var cell = FindParentCell(last);
            foreach (var i in cell.FindElements(By.TagName("a")))
            {
                yield return new VideoCell(i, this.webDriver);
            }
        }

        public IEnumerable<int> GetLastDaysBookmarkedVideosIndex(int last)
        {
            var cell = FindParentCell(last);
            var allPositions = cell.FindElements(By.TagName("a"));
            List<int> ret = new List<int>();
            for (int i = 0; i < allPositions.Count; i++)
            {
                if (allPositions[i].GetAttribute("class").Contains("bookmarked"))
                {
                    ret.Add(i);
                }
            }
            return ret;
        }

        private IWebElement FindParentCell(int last)
        {
            if (last < 1 || last > 10)
            {
                throw new Exception("Out of range!!");
            }
            string title = string.Empty;
            if (last == 1)
            {
                title = "Yesterday's";
            }
            else
            {
                DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
                dt = TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
                dt = dt.AddDays(last * (-1));
                title = dt.ToString("D");
            }

            var allSections = rootElement.FindElements(By.XPath("//div[@class='section']"));
            var cell = (from i
                      in allSections
                        where i.FindElement(By.TagName("h4")).Text.Equals(title, StringComparison.OrdinalIgnoreCase)
                        select i).Single();
            return cell;
        }

        public VideoDrawer GetVideoDrawer()
        {
            return new VideoDrawer(this.rootElement.FindElement(By.ClassName("drawer")), this.webDriver);
        }
    }
}
