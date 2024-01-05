using NUnit.Framework;
using System;
using System.IO;
using GTAutomation.Framework.Helpers;
using GTAutomation.Framework.Utilities;
using GTAutomation.Entities;

namespace GTAutomation.Framework.Configurations
{
    public class Configuration : SeleniumHelpers
    {        
        public string NickName;
        public string Password;       
        public static string screenShotPath;
        public static string TimeStamp = null;
        public string UseBrowser = JsonReader.Get("UseBrowser");
        public static string ProjectUrl = JsonReader.Get("ProjectUrl");
        public static string APIEndPoint = JsonReader.Get("APIEndPoint");
        public static string BrowserVersion = JsonReader.Get("BrowserVersion");
        public bool HeadlessMode = bool.Parse(JsonReader.Get("HeadlessMode"));
        public static string ReportFilePath = JsonReader.Get("ReportFilePath");
        public string CredentialDataFilePath = JsonReader.Get("CredentialDataFilePath");

        [OneTimeSetUp]
        public void IntializeReport()
        {
            if(TimeStamp == null)
            {
                TimeStamp = DateTime.Now.ToString().Replace(" ", "_").Replace("-", "_").Replace(":", "_");
                ReportFilePath = ReportFilePath + "_" + TimeStamp;
            }

            if(!Directory.Exists(ReportFilePath))
            {
                Directory.CreateDirectory(ReportFilePath);
            }

            screenShotPath = ReportFilePath + Path.DirectorySeparatorChar + "ScreenShot" + Path.DirectorySeparatorChar;

            if(!Directory.Exists(screenShotPath))
            {
                Directory.CreateDirectory(screenShotPath);
            }
        }

        [SetUp]
        public void SetupCredential()
        {
            var credential = (Credential)JsonReader.JsonReadData(typeof(Credential), CredentialDataFilePath);

            NickName = credential.NickName;
            Password = credential.Password;
        }
    }
}
