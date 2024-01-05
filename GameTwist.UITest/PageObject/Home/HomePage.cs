using OpenQA.Selenium;
using GTAutomation.Framework.DriverManager;
using AventStack.ExtentReports;
using GTAutomation.Framework.Helpers;

namespace GTAutomation.PageObject.Login
{
    public class HomePage : BaseDriver
    {
        SeleniumHelpers seleniumHelpers;
        private IWebDriver driver;
        private ExtentTest test;

        private By loginButton => By.XPath("//a[text()='Login']");
        private By registerButton => By.XPath("//a[text()='Register']");
        private By profileUserName => By.XPath("//div[contains(@class,'status__username')]");

        public HomePage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            seleniumHelpers = new SeleniumHelpers(driver);
        }

        public void ClickLogin()
        {
            seleniumHelpers.WaitForElement(loginButton);
            driver.FindElement(loginButton).Click();
        }

        public void ClickRegister()
        {
            seleniumHelpers.WaitForElement(registerButton);
            driver.FindElement(registerButton).Click();
        }

        public string GetProfileName()
        {
            seleniumHelpers.WaitForElement(profileUserName);
            return driver.FindElement(profileUserName).Text;
        }

    }
}
