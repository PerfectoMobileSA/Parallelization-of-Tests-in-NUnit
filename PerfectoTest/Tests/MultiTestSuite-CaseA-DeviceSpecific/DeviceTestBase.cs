using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using PerfectoTest.PerfectoAPI;
using PerfectoTest.Utils;
using Reportium.client;
using Reportium.model;
using Reportium.test.Result;
using Reportium.test;


namespace PerfectoTest.Tests
{
    //adding the devices that will run the test in parallel
    [TestFixture(Device.Device1)]
    [TestFixture(Device.Device2)]
    [TestFixture(Device.Device3)]
    [Parallelizable(ParallelScope.Self)]
    public class TestBase
    {
        protected RemoteWebDriver rmDriver;
        private string deviceName;
        protected ReportiumClient reportiumClient;
        protected String errorMsg;
        protected DateTime date;

        public TestBase(string deviceName)
        {
            this.deviceName = deviceName;
        }

        [SetUp]
        public void SetupDriver()
        {
            SetupDriver(deviceName);
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
        
        private void SetupDriver(string deviceId)
        {
            if (!WaitForDeviceAvailable(PerfectoUrl.PrivateCloud + deviceId + PerfectoUrl.DeviceInfo))
            {
                throw new Exception("The device -" + deviceId + "- is not available after many times tried to reach it.");
            }
            else
            {
                var capabilities = new DesiredCapabilities();
                capabilities.SetCapability("deviceName", deviceId);
                capabilities.SetCapability("securityToken", Access.SecurityToken);
                capabilities.SetPerfectoLabExecutionId(Access.Host);
                var url = new Uri($"https://{Access.Host}/nexperience/perfectomobile/wd/hub");
                rmDriver = new RemoteWebDriver(new HttpCommandExecutor(url, TimeSpan.FromSeconds(40)), capabilities);
            }
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


        private bool WaitForDeviceAvailable(string requestUrl)
        {
            int index = 0;
            int interval = 3;

            while (index != interval)
            {
                index++;
                XmlHandler getDeviceAvailability = new XmlHandler(requestUrl);
                if (getDeviceAvailability.IsDeviceAvailable())
                {
                    return true;
                }
                else
                {
                    index++;
                    if (index < interval) Thread.Sleep((index + 1) * 10000);
                }
            }

            return false;
        }
    }
}
