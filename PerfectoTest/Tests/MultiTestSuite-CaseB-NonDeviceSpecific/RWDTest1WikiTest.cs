using System;
using NUnit.Framework;
using OpenQA.Selenium;
using PerfectoTest.Utils;
using Reportium.test;

namespace PerfectoTest.Tests
{
    [TestFixture]
    public class RWDTest1WikiTest : RemoteWebDriverBase
    {
        [Test]
        public void WikiTest()
        {
            try
            {
                Assert.IsNotNull(rmDriver);
                rmDriver.Navigate().GoToUrl(UIUrl.WikiTestWebUrl);
                rmDriver.FindElement(By.CssSelector("#js-link-box-en > strong:nth-of-type(1)")).Click();
                IWebElement link = rmDriver.FindElement(By.CssSelector("#mainpage > h2:nth-of-type(1)"));
                Assert.IsTrue(link.Displayed);
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}
