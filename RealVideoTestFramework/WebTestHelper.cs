using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTest.TestUtilities;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using PageObjectFactory = Selenium.Tools.PageFactory;
using OpenQA.Selenium.Remote;

namespace RealVideo.WebTest.TestFramework
{
    public class WebTestHelper : TestHelper
    {
        private const string RealVideoURL = "http://videos.real.com/rp/web_videos?u=demo";
        private const string UIMappFilePath = @"..\..\..\RealVideoTestFramework\UIMaps";

        public RemoteWebDriver Driver { get; private set; }

        public override void OnHelperCreation(TestBase test)
        {
            base.OnHelperCreation(test);

            this.Driver = new InternetExplorerDriver();
            this.Driver.Navigate().GoToUrl(RealVideoURL);
            PageObjectFactory.UIMapFilePath = UIMappFilePath;

            test.AddTestCleanup("UnBookmark all videos", () =>
            {
                NavigationBar nav = test.Get<NavigationBar>(Driver);
                nav.GoToBookmarkPage();

                BookmarkPage bookmarkpage = Test.Get<BookmarkPage>(Driver);
                var allVideos = bookmarkpage.GetAllVideos().ToArray();
                foreach (var video in allVideos)
                {
                    video.Bookmark();
                }
            });
        }
    }
}
