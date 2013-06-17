using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using RealVideo.WebTest.TestFramework;

namespace RealVideo.WebTest.TestCase
{
    [TestFixture]
    public class WebTest : WebTestBase
    {
        //Verify if there is no any bookmarked videos in bookmard page
        [Test]
        [TestCase]
        public void VerifyNoBookmarkedVideos()
        {
            //1. Go to bookmark page
            var navBar = Get<NavigationBar>(WebTestHelper.Driver);
            navBar.GoToBookmarkPage();

            //2. Verify if no video in bookmard page
            BookmarkPage bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            Assert.AreEqual(0, bookmarkPage.GetAllVideos().ToArray().Length, "The count of videos in bookmark page should be 0.");
        }

        //Verify if the first video can be bookmarked in top five row
        [Test]
        [TestCase]
        public void VerifyTheFirstBookmarkedVideoInTopFive()
        { 
            //1. Bookmark first video in Top5
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var firstVideosCellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray()[0];
            firstVideosCellInDailyVideoPage.Bookmark();

            //2. Verify if it bookmarked successfully in video page
            var bookmarkedVideosInDailyVideoPage = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(1, bookmarkedVideosInDailyVideoPage.Length, "The count of bookmarked videos in video page should be 1.");
            
            //3. Verify if the first video is bookmarked in Top5
            Assert.AreEqual(0, bookmarkedVideosInDailyVideoPage[0], "The bookmarked video in video page should be the first one in the Top 5 row.");

            //4. Go to bookmark page, verify if the correct video is bookmarked
            var navBar = Get<NavigationBar>(WebTestHelper.Driver);
            navBar.GoToBookmarkPage();

            var bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            var bookmarkedVidesInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            Assert.AreEqual(1, bookmarkedVidesInBookmarkPage.Length, "The count of bookmarked videos in bookmark page should be 1.");

            //5. Compare video in Daily page and in Bookmark page
            var firstVideosCellInBookmarkPage = bookmarkedVidesInBookmarkPage[0];
            Assert.AreEqual(firstVideosCellInDailyVideoPage.ImageSrc, firstVideosCellInBookmarkPage.ImageSrc, "The image src of video should be same.");
            Assert.AreEqual(firstVideosCellInDailyVideoPage.Title, firstVideosCellInBookmarkPage.Title, "The title of video should be same");
        }

        //Verify if the last video can be bookmarked in top five row
        [Test]
        [TestCase]
        public void VerifyTheLastBookmarkedVideoInTopFive()
        { 
            //1. Bookmark last video in Top5
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var lastVideosCellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray()[4];
            lastVideosCellInDailyVideoPage.Bookmark();

            //2. Verify if it bookmarked successfully in video page
            var bookmarkedVideosInDailyVideoPage = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(1, bookmarkedVideosInDailyVideoPage.Length, "The count of bookmarked videos in video page should be 1.");
            
            //3. Verify if the last video is bookmarked in Top5
            Assert.AreEqual(4, bookmarkedVideosInDailyVideoPage[0], "The bookmarked video in video page should be the last one in the Top 5 row.");

            //4. Go to bookmark page, verify if the correct video is bookmarked
            var navBar = Get<NavigationBar>(WebTestHelper.Driver);
            navBar.GoToBookmarkPage();

            var bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            var bookmarkedVidesInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            Assert.AreEqual(1, bookmarkedVidesInBookmarkPage.Length, "The count of bookmarked videos in bookmark page should be 1.");

            //5. Compare video in Daily page and in Bookmark page
            var lastVideosCellInBookmarkPage = bookmarkedVidesInBookmarkPage[0];
            Assert.AreEqual(lastVideosCellInDailyVideoPage.ImageSrc, lastVideosCellInBookmarkPage.ImageSrc, "The image src of video should be same.");
            Assert.AreEqual(lastVideosCellInDailyVideoPage.Title, lastVideosCellInBookmarkPage.Title, "The title of video should be same");
        }

        //Verify if corrent videos can be marked into bookmark page, and the order should be correct
        [Test]
        [TestCase]
        public void VerifyTheOrderOfBookmarkedVideo()
        { 
            //1. Bookmark all videos in Top5
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            foreach (var cell in CellInDailyVideoPage)
            {
                cell.Bookmark();
            }

            //2. Verify if there are five videos are marked in daily page
            var bookmarkedVideosInDailyVideoPage = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(5, bookmarkedVideosInDailyVideoPage.Length, "The count of bookmarked videos in video page should be 5.");

            //3. Verify if all five videos are marked in order
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i, bookmarkedVideosInDailyVideoPage[i], "The video "+ i +" should be marked.");
            }

            //4. Go to bookmark page
            var navBar = Get<NavigationBar>(WebTestHelper.Driver);
            navBar.GoToBookmarkPage();

            //5. Verify if there are five videos in the bookmark page
            var bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            var bookmarkedVidesInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            Assert.AreEqual(5, bookmarkedVidesInBookmarkPage.Length, "The count of bookmarked videos in bookmark page should be 5.");

            Stack<VideoCell> cells = new Stack<VideoCell>(bookmarkedVidesInBookmarkPage);
            bookmarkedVidesInBookmarkPage = cells.ToArray();

            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(CellInDailyVideoPage[i].ImageSrc, bookmarkedVidesInBookmarkPage[i].ImageSrc, "The image src of video " +i+ " should be same");
                Assert.AreEqual(CellInDailyVideoPage[i].Title, bookmarkedVidesInBookmarkPage[i].Title, "The title of video " + i + " should be same");
            }
        }

        //Verify if the unbookmark can work in daily video page
        [Test]
        [TestCase]
        public void VerifyUnBookMarkInDailyVideoPage()
        { 
            //1. Bookmark all videos in Top5
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            foreach (var cell in CellInDailyVideoPage)
            {
                cell.Bookmark();
            }

            //2. Verify if there are five videos are marked in daily page
            var bookmarkedVideosInDailyVideoPage = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(5, bookmarkedVideosInDailyVideoPage.Length, "The count of bookmarked videos in video page should be 5.");

            //3. Verify if all five videos are marked in order
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(i, bookmarkedVideosInDailyVideoPage[i], "The video "+ i +" should be marked.");
            }

            //4. Unbookmark one video in daily page
            CellInDailyVideoPage[0].Bookmark();

            //5. Verify if unbookmark can work
            bookmarkedVideosInDailyVideoPage = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(4, bookmarkedVideosInDailyVideoPage.Length, "The count of bookmarked videos in video page should be 4, after unmarking one video.");

            //6. Verify the first one is unmarked
            Assert.AreEqual(1, bookmarkedVideosInDailyVideoPage[0]);
            Assert.AreEqual(2, bookmarkedVideosInDailyVideoPage[1]);
            Assert.AreEqual(3, bookmarkedVideosInDailyVideoPage[2]);
            Assert.AreEqual(4, bookmarkedVideosInDailyVideoPage[3]);
        }

        //Verify if the unbookmark can work in bookmark page
        [Test]
        [TestCase]
        public void VerifyUnBookMarkInBookmarkPage()
        { 
            //1. Bookmark all videos in Top5
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            foreach (var cell in CellInDailyVideoPage)
            {
                cell.Bookmark();
            }

            //2. Go to bookmark page
            NavigationBar navVar = Get<NavigationBar>(WebTestHelper.Driver);
            navVar.GoToBookmarkPage();

            //3. Unbookmark one video in bookmark page
            BookmarkPage bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            var allMarkedVideos = bookmarkPage.GetAllVideos().ToArray();
            allMarkedVideos[0].Bookmark();

            //4. Verify if unbookmark work
            allMarkedVideos = bookmarkPage.GetAllVideos().ToArray();
            Assert.AreEqual(4, allMarkedVideos.Length, "There should be 4 videos left in bookmarked page");

            //5. Go back to daily videos page
            navVar.GoToDailyVideoPage();
            
            //6. Verify if one video are unmarked
            var bookmarkedVideosInDailyVideoPage = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();

            //7. Verify the last video is unmarked in daily video page
            Assert.AreEqual(4, bookmarkedVideosInDailyVideoPage.Length, "There should be 4 videos left in daily video page");
            Assert.AreEqual(0, bookmarkedVideosInDailyVideoPage[0]);
            Assert.AreEqual(1, bookmarkedVideosInDailyVideoPage[1]);
            Assert.AreEqual(2, bookmarkedVideosInDailyVideoPage[2]);
            Assert.AreEqual(3, bookmarkedVideosInDailyVideoPage[3]);
        }

        //Verify if the bookmark button in drawer can work in daily page
        [Test]
        [TestCase]
        public void VerifyDrawerBookmarkButtonInDailyPage()
        {
            //1. Play one video
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            CellInDailyVideoPage[0].Play();

            //2. Verify the button is bookmark
            VideoDrawer drawer = dailVideoPage.GetVideoDrawer();
            Assert.AreEqual(false, drawer.IsBookMarked, "The button should be bookmark button");

            //3. Click bookmark button in drawer
            drawer.BookMark();

            //TODO: Need to add a retry logic to wait bookmark button change to unbookmark button
            Thread.Sleep(1000);

            //4. Verify if button change to unbookmark
            Assert.AreEqual(true, drawer.IsBookMarked, "The button should be unbookmark button");

            //5. Verify if bookmark icon in preview image should be displayed
            var bookmarkedVideoIndex = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(1, bookmarkedVideoIndex.Length, "There should be only one marked video");
            Assert.AreEqual(0, bookmarkedVideoIndex[0], "The first video should be marked");
        }

        //Verify if the unbookmark button in drawer can work in daily page
        [Test]
        [TestCase]
        public void VerifyDrawerUnBookmarkButtonInDailyPage()
        {
            //1. Bookamrk and then play a video
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            CellInDailyVideoPage[0].Bookmark();
            CellInDailyVideoPage[0].Play();

            //2. Verify the button is bookmark
            VideoDrawer drawer = dailVideoPage.GetVideoDrawer();
            Assert.AreEqual(true, drawer.IsBookMarked, "The button should be unbookmark button");

            //3. Click unbookmark button in drawer
            drawer.UnBookMark();

            //TODO: Need to add a retry logic to wait bookmark button change to unbookmark button
            Thread.Sleep(1000);

            //4. Verify if button change to unbookmark
            Assert.AreEqual(false, drawer.IsBookMarked, "The button should be bookmark button");

            //5. Verify if bookmark icon in preview image should NOT be displayed
            var bookmarkedVideoIndex = dailVideoPage.GetTodayTopBookmarkedVideosIndex().ToArray();
            Assert.AreEqual(0, bookmarkedVideoIndex.Length, "No one should be marked");
        }

        //Verify if the unbookmark button in drawer can work in bookmark page
        [Test]
        [TestCase]
        public void VerifyDrawerUnBookmarkButtonInBookmarkPage()
        {
            //1. Bookamrk a video
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            CellInDailyVideoPage[0].Bookmark();

            //2. Go to bookamrk page
            NavigationBar nav = Get<NavigationBar>(WebTestHelper.Driver);
            nav.GoToBookmarkPage();

            //3. Verify if button is bookmark
            var bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            var CellInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            CellInBookmarkPage[0].Play();

            VideoDrawer drawer = bookmarkPage.GetVideoDrawer();
            Assert.AreEqual(true, drawer.IsBookMarked, "The button should be unbookmark button");

            //4. Click unbookmark button in drawer
            drawer.UnBookMark();

            //TODO: Need to add a retry logic to wait bookmark button change to unbookmark button
            Thread.Sleep(1000);

            //5. Verify if button change to unbookmark
            Assert.AreEqual(false, drawer.IsBookMarked, "The button should be bookmark button");

            //6. Click refresh button
            bookmarkPage.Reresh();

            //7. Verify no video in the bookmark page
            CellInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            Assert.AreEqual(0, CellInBookmarkPage.Length, "After unbookmarking, there should be no one video in the bookmark page");
        }

        //Verify if the bookmark button in drawer can work in bookmark page
        [Test]
        [TestCase]
        public void VerifyDrawerBookmarkButtonInBookmarkPage()
        {
            //1. Bookamrk a video
            DailyVidoesPage dailVideoPage = Get<DailyVidoesPage>(WebTestHelper.Driver);
            var CellInDailyVideoPage = dailVideoPage.GetTodayTopFiveVideos().ToArray();
            CellInDailyVideoPage[0].Bookmark();

            //2. Go to bookamrk page
            NavigationBar nav = Get<NavigationBar>(WebTestHelper.Driver);
            nav.GoToBookmarkPage();

            //3. Verify if button is bookmark
            var bookmarkPage = Get<BookmarkPage>(WebTestHelper.Driver);
            var CellInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            CellInBookmarkPage[0].Play();

            //4. Unbookmark video
            CellInBookmarkPage[0].Bookmark();

            //TODO: Need to add a retry logic to wait bookmark button change to unbookmark button
            Thread.Sleep(1000);

            //5. Verify if bookmark button is shown
            VideoDrawer drawer = bookmarkPage.GetVideoDrawer();
            Assert.AreEqual(false, drawer.IsBookMarked, "The button should be bookmark button");

            //6. Click refresh button
            bookmarkPage.Reresh();

            //7. Verify no video in the bookmark page
            CellInBookmarkPage = bookmarkPage.GetAllVideos().ToArray();
            Assert.AreEqual(0, CellInBookmarkPage.Length, "After unbookmarking, there should be no one video in the bookmark page");
        }
    }
}
