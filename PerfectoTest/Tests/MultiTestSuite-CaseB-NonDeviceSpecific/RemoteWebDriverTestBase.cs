using System;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using PerfectoTest.Attributes;
using PerfectoTest.PerfectoAPI;
using Reportium.client;
using Reportium.model;
using Reportium.test.Result;
using Reportium.test;

namespace PerfectoTest.Tests
{
    // Need to set [assembly: LevelOfParallelism(x)] in Properties/AssemblyInfo.cs
    [Parallelizable(ParallelScope.Self)]
    public class RemoteWebDriverBase
    {
        protected RemoteWebDriver rmDriver;
        protected ReportiumClient reportiumClient;
        protected String errorMsg;
        protected DateTime date;

        [SetUp]
        public void SetupTest()
        {
            SetupDriver();
            SetupReporting(rmDriver);
            reportiumClient.testStart(TestContext.CurrentContext.Test.FullName, new TestContextTags("C# Test"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            if (errorMsg != null)
                reportiumClient.testStop(TestResultFactory.createFailure(errorMsg, null));
            else
            {
                reportiumClient.testStop(TestResultFactory.createSuccess());
            }
            rmDriver.Close();
            rmDriver.Quit(); 
        }

        private void SetupDriver() {
            var capabilities = new DesiredCapabilities();
            capabilities.SetCapability("securityToken", Access.SecurityToken);
            string expectedPlatform = TestAttributes.Get<DevicePlatformExpectationAttribute>()?.DeviceOS;
            string expectedDevice = TestAttributes.Get<DeviceExpectationAttribute>()?.DeviceName;

            if (expectedPlatform != null)
            {
                capabilities.SetCapability("platformName", expectedPlatform);
            }
            else if (expectedDevice != null)
            {
                capabilities.SetCapability("deviceName", expectedDevice);
            }
            else
            {
                capabilities.SetCapability("platformName", Utils.DevicePlatform.Android + "|" + Utils.DevicePlatform.iOS);
            }
            var url = new Uri($"https://{Access.Host}/nexperience/perfectomobile/wd/hub");
            rmDriver = new RemoteWebDriver(new HttpCommandExecutor(url, TimeSpan.FromSeconds(40)), capabilities);
        }

        private void SetupReporting(RemoteWebDriver repDriver)
        {
            date = DateTime.Now;
            int dateToJob = date.Year * 10000 + this.date.Month * 100 + this.date.Day;

            PerfectoExecutionContext perfectoExecutionContext = new PerfectoExecutionContext.PerfectoExecutionContextBuilder()
                   .withProject(new Project("Perfecto Sample Project", "v1.0")) //Here you  can also parse the Project name from your CI tool
                   .withContextTags(new[] { "Perfecto", "Sample", "C#" }) //Here you can also parse the Tags from your CI tool
                   .withJob(new Job("Sample C# Job", dateToJob)) //Here you  can also parse the Build Number from your CI tool
                   .withWebDriver(repDriver)
                   .build();
            reportiumClient = PerfectoClientFactory.createPerfectoReportiumClient(perfectoExecutionContext);
        }
    }
}
