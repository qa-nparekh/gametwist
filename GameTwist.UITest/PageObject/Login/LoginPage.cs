using OpenQA.Selenium;
using GTAutomation.Framework.DriverManager;
using GTAutomation.Framework.Helpers;
using AventStack.ExtentReports;

namespace GTAutomation.PageObject.Login
{
    public class LoginPage : BaseDriver
    {
        SeleniumHelpers seleniumHelpers;
        private IWebDriver driver;
        private ExtentTest test;

        private By nickNameTextBox => By.Id("username");
        private By passwordTextBox => By.Id("password");
        private By loginButton => By.XPath("//button[contains(.,'Log in')]");
        private By forgottenYourPasswordButton => By.XPath("//a[contains(.,'Forgotten your password?')]");
        private By invalidNickNamePasswordValidation => By.XPath("//form//ul/li");
        private By nickNameValidation => By.XPath("//input[@name='username']/following::li[1]");
        private By passwordValidation => By.XPath("//input[@name='password']/following::li[1]");

        public LoginPage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            seleniumHelpers = new SeleniumHelpers(driver);
        }

        public void TypeNickName(string nickName)
        {
            seleniumHelpers.WaitForElement(nickNameTextBox);
            SeleniumHelpers.EnterText(driver.FindElement(nickNameTextBox), nickName);
        }

        public void TypePassword(string password)
        {
            SeleniumHelpers.EnterText(driver.FindElement(passwordTextBox), password);
        }

        public void ClickLogin()
        {
            seleniumHelpers.WaitForElement(loginButton);
            driver.FindElement(loginButton).Click();
        }

        public void LoginWithCredential(string nickName, string password)
        {
            TypeNickName(nickName);
            TypePassword(password);
            ClickLogin();
        }

        public string GetIncorrectNickNamePasswordValidation()
        {
            return SeleniumHelpers.GetText(driver.FindElement(invalidNickNamePasswordValidation));
        }

        public string GetNickNameValidation()
        {
            seleniumHelpers.WaitForElement(nickNameValidation);
            return driver.FindElement(nickNameValidation).Text;
        }

        public string GetPasswordValidation()
        {
            seleniumHelpers.WaitForElement(passwordValidation);
            return driver.FindElement(passwordValidation).Text;
        }

        public void ClickForgottenYourPassword()
        {
            seleniumHelpers.WaitForElement(forgottenYourPasswordButton);
            driver.FindElement(forgottenYourPasswordButton).Click();
        }

    }
}
