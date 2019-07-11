using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PerfectoTest.Utils;

namespace PerfectoTest.Tests
{
    public class TestSuite1 : TestBase
    {
        public TestSuite1(string deviceName) : base(deviceName)
        {
        }

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
