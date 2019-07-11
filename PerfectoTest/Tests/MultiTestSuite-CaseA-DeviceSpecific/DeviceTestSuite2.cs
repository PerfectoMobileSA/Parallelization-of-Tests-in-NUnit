using System;
using NUnit.Framework;
using OpenQA.Selenium;
using PerfectoTest.Utils;

namespace PerfectoTest.Tests
{
    public class TestSuite2 : TestBase
    {
        public TestSuite2(string deviceName) : base(deviceName)
        {
        }

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

        [Test]
        public void PerfectoSiteTest()
        {
            try
            {
                Assert.IsNotNull(rmDriver);
                rmDriver.Navigate().GoToUrl(UIUrl.PerfectoWebUrl);
                
                rmDriver.FindElement(By.CssSelector("#block-perfecto-branding > a:nth-child(1) > img:nth-child(1)")).Click();
                IWebElement link = rmDriver.FindElement(By.CssSelector("#block-perfecto-branding > a:nth-child(1) > img:nth-child(1)"));
                Assert.IsTrue(link.Displayed);
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }

    }
}
