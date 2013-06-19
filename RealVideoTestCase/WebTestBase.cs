using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTest.TestUtilities;
using RealVideo.WebTest.TestFramework;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace RealVideo.WebTest.TestCase
{
    public class WebTestBase : TestBase
    {
        protected WebTestHelper WebTestHelper;

        public override void OnTestInitialize()
        {
            base.OnTestInitialize();
            WebTestHelper = Get<WebTestHelper>();
        }

        public override void OnTestFixtureCleanup()
        {
            base.OnTestFixtureCleanup();
            WebTestHelper.Driver.Quit();
        }
    }
}
