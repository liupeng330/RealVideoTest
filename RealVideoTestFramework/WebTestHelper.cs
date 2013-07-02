using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTest.TestUtilities;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using PageObjectFactory = Selenium.Tools.PageFactory;

namespace RealVideo.WebTest.TestFramework
{
    public class WebTestHelper : TestHelper
    {
        private const string UIMappFilePath = @"..\..\..\RealVideoTestFramework\UIMaps";
        private const string RealVideoURL = "http://videos.real.com/rp/web_videos?u=demo";

        public RemoteWebDriver Driver { get; private set; }

        public override void OnHelperCreation(TestBase test)
        {
            base.OnHelperCreation(test);

            this.Driver = new FirefoxDriver();
            this.Driver.Manage().Window.Maximize();
            this.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            this.Driver.Navigate().GoToUrl(RealVideoURL);

            PageObjectFactory.UIMapFilePath = UIMappFilePath;

            test.AddTestCleanup("UnBookmark all videos", () =>
            {
                NavigationBar nav = test.Get<NavigationBar>(Driver);
                nav.GoToBookmarkPage();

                BookmarkPage bookmarkpage = test.Get<BookmarkPage>(Driver);
                var allVideos = bookmarkpage.GetAllVideos().ToArray();
                foreach (var video in allVideos)
                {
                    video.Bookmark();
                }

                nav.GoToDailyVideoPage();
            });
        }
    }
}
