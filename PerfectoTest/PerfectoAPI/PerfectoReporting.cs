using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Reportium.test;
using Reportium.test.Result;
using Reportium.client;
using Reportium.model;


namespace PerfectoTest.PerfectoAPI
{
    class Reportium
    {
        private static ReportiumClient CreateReportingClient(RemoteWebDriver driver)
        {
            PerfectoExecutionContext perfectoExecutionContext = new PerfectoExecutionContext.PerfectoExecutionContextBuilder()
               .withProject(new Project("Perfecto Sample Project", "v1.0")) //optional
               .withContextTags(new[] { "Perfecto", "Sample", "C#" }) //optional
               .withJob(new Job("Sample C# Job", 1)) //optional
               .withWebDriver(driver)
               .build();
            return PerfectoClientFactory.createPerfectoReportiumClient(perfectoExecutionContext);
        }
    }
}

