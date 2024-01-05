using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using GTAutomation.Framework.Configurations;
using GTAutomation.Framework.Helpers;
using GTAutomation.Framework.Report;
using GTAutomation.Framework.Utilities;
using WebDriverManager.DriverConfigs.Impl;

namespace GTAutomation.Framework.DriverManager
{
    public class BaseDriver : Configuration
    {
        protected IWebDriver webDriver;

        /// <summary>
        /// Initialize the webDriver
        /// </summary>
        /// <param name="test"></param>
        /// <returns>webDriver</returns>
        public IWebDriver InitializeWebDriver(ExtentTest test)
        {
            switch (UseBrowser)
            {
                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions options = new ChromeOptions();

                    if (HeadlessMode)
                    {
                        options.AddArguments("--headless");
                        options.AddArguments("start-maximized");
                    }

                    string DownloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "download");
                    options.AddUserProfilePreference("download.default_directory", DownloadPath);

                    if (BrowserVersion == "")
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    }
                    else
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), BrowserVersion);
                    }

                    webDriver = new ChromeDriver(options);

                    break;
            }

            if (webDriver != null)
            {
                webDriver.Navigate().GoToUrl(ProjectUrl);
                webDriver.Manage().Window.Maximize();
                test.Log(Status.Pass, "Open " + ProjectUrl + " in " + UseBrowser + " browser.");

                SeleniumHelpers seleniumHelpers = new SeleniumHelpers(webDriver);
                seleniumHelpers.SetImplicitWait();
            }
            return webDriver;
        }

        /// <summary>
        /// To get instance of webDriver
        /// </summary>
        /// <returns>webDriver</returns>
        public IWebDriver GetWebDriver()
        {
            return webDriver;
        }

        /// <summary>
        /// To close browser
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="test"></param>
        public void CloseBrowser(IWebDriver driver , ExtentTest test)
        {
            driver.Quit();
            test.Log(Status.Pass, UseBrowser + " browser closed successfully.");
        }

        /// <summary>
        /// Capture Screenshot
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public MediaEntityModelProvider GetScreenShot(IWebDriver driver)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot captureScreenshot = screenshotDriver.GetScreenshot();
            var screenshotName = TestContext.CurrentContext.Test.MethodName + DateTimes.CurrentTimestamp().ToString() + ".png" ;
            captureScreenshot.SaveAsFile(screenShotPath + screenshotName, ScreenshotImageFormat.Png);
            return MediaEntityBuilder.CreateScreenCaptureFromPath("ScreenShot\\" + screenshotName).Build();
        }

        [TearDown]
        public void GenerateReport()
        {
            ExtentTestManager.GetTest().Extent.Flush();
        }
    }
}
