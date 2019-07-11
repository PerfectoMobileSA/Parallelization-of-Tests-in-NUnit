using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PerfectoTest.Attributes;
using PerfectoTest.Utils;
using Reportium.test;
using DevicePlatform = PerfectoTest.Utils.DevicePlatform;

namespace PerfectoTest.Tests
{
    [TestFixture]
    public class RWDTest3GoogleTest : RemoteWebDriverBase
    {
        [DevicePlatformExpectation(DevicePlatform.Android)]
        [Test]
        public void GoogleTest()
        {
            try
            {
                Assert.IsNotNull(rmDriver);
                rmDriver.Navigate().GoToUrl(UIUrl.GoogleWebUrl);
                var element = new WebDriverWait(rmDriver, TimeSpan.FromSeconds(30)).Until(condition: ExpectedConditions.ElementToBeClickable(By.Name("q")));
                element.SendKeys("Perfecto Mobile");
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
        }
    }
}
