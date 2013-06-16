using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebTest.TestUtilities;
using RealVideo.WebTest.TestFramework;

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
    }
}
