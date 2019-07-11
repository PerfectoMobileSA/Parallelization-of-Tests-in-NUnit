using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PerfectoTest.Attributes;
using PerfectoTest.Utils;
using Reportium.test;
using DevicePlatform = PerfectoTest.Utils.DevicePlatform;
using Assert = NUnit.Framework.Assert;

namespace PerfectoTest.Tests
{
    [TestFixture]
    public class RWDTest2PerfectoSupportTest : RemoteWebDriverBase
    {
        [DevicePlatformExpectation(DevicePlatform.iOS)]
        [Test]
        public void PerfectoSupportTest()
        {
            try
            {
                Assert.IsNotNull(rmDriver);
                rmDriver.Navigate().GoToUrl(UIUrl.PerfectoSupportWebUrl);
                var cloudIcon = new WebDriverWait(rmDriver, TimeSpan.FromSeconds(30)).Until(condition: ExpectedConditions.ElementIsVisible(By.CssSelector("span.fa-cloud")));
                cloudIcon.Click();
                var configIcon = new WebDriverWait(rmDriver, TimeSpan.FromSeconds(30)).Until(condition: ExpectedConditions.ElementIsVisible(By.CssSelector("span.fa-cogs")));
                configIcon.Click();
                var contactContainer = new WebDriverWait(rmDriver, TimeSpan.FromSeconds(30)).Until(condition: ExpectedConditions.ElementIsVisible(By.CssSelector("#contactSupport")));
                Assert.IsTrue(contactContainer.Displayed);
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}
