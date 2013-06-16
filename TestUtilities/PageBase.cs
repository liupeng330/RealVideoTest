using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace WebTest.TestUtilities
{
    public class PageBase
    {
        protected IWebDriver webDriver;

        public PageBase() { }

        public PageBase(IWebDriver driver)
        {
            this.webDriver = driver;
        }
    }
}
