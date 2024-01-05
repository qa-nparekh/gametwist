using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using GTAutomation.Framework.Configurations;
using AventStack.ExtentReports.Reporter.Configuration;

namespace GTAutomation.Framework.Report
{
    public class ExtentService : Configuration
    {
        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());
        
        public static ExtentReports Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        static ExtentService()
        {
            var reporter = new ExtentHtmlReporter(ReportFilePath + Path.DirectorySeparatorChar);
            reporter.Config.Theme = Theme.Standard;
            Instance.AttachReporter(reporter);
        }

        private ExtentService()
        {
        }
    }
}
