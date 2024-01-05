using OpenQA.Selenium;
using GTAutomation.Framework.DriverManager;
using GTAutomation.Framework.Helpers;
using AventStack.ExtentReports;

namespace GTAutomation.PageObject.Login
{
    public class RegistrationPage : BaseDriver
    {
        SeleniumHelpers seleniumHelpers;

        private IWebDriver driver;
        private ExtentTest test;

        private By emailTextBox => By.XPath("//input[@type='email']");
        private By nickNameTextBox => By.XPath("//input[@name='nickname']");
        private By passwordTextBox => By.CssSelector("[type='password']");
        private By dayDropDown => By.CssSelector("[name='day']");
        private By monthDropDown => By.CssSelector("[name='month']");
        private By yearDropDown => By.CssSelector("[name='year']");
        private By recaptchaCheckBox => By.XPath("//label[text()=\"I'm not a robot\"]");
        private By termAndConditionCheckBox => By.Id("termsAccept");
        private By beginAdventureButton => By.CssSelector("[type='submit']");
        private By emailValidation => By.XPath("//input[@type='email']/following::li[1]");
        private By nickNameValidation => By.XPath("//input[@name='nickname']/following::li[1]");
        private By passwordValidation => By.XPath("//input[@name='password']/following::li[1]");
        private By dateofbirthValidation => By.XPath("//ul[contains(.,'Please select dayPlease select monthPlease select year')]]");
        private By recaptchaValidation => By.XPath("//li[text()='The security check is a required field. Please enter the code.']");
        private By termsAndConditionValidation => By.XPath("//input[@id='termsAccept']/following::li[1]");


        public RegistrationPage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            seleniumHelpers = new SeleniumHelpers(driver);
        }

        public void TypeEmail(string email)
        {
            seleniumHelpers.WaitForElement(emailTextBox);
            SeleniumHelpers.EnterText(driver.FindElement(emailTextBox), email);
        }

        public void TypeNickName(string nickName)
        {
            SeleniumHelpers.EnterText(driver.FindElement(nickNameTextBox), nickName);
        }

        private void TypePassword(string password)
        {
            SeleniumHelpers.EnterText(driver.FindElement(passwordTextBox), password);
        }

        private void SelectDateOfBirth(string day,string month,string year)
        {
            seleniumHelpers.SelectDropDownByText(dayDropDown,day);
            SeleniumHelpers.StaticWait(2);
            seleniumHelpers.SelectDropDownByText(monthDropDown,month);
            SeleniumHelpers.StaticWait(2);
            seleniumHelpers.SelectDropDownByText(yearDropDown,year);
            SeleniumHelpers.StaticWait(2);
        }

        private void AcceptRecaptcha()
        {
            driver.FindElement(recaptchaCheckBox).Click();
        }

        private void AcceptTermAndCondition()
        {
            driver.FindElement(termAndConditionCheckBox).Click();
        }

        public void ClickBeginAdventure()
        {
            driver.FindElement(beginAdventureButton).Click();
        }

        public void TypeAccountSetupDetails(string email, string nickName, string password , string day, string month, string year)
        {
            TypeEmail(email);
            TypeNickName(nickName);
            TypePassword(password);
            SelectDateOfBirth(day,month,year);            
            AcceptTermAndCondition();
            /*AcceptRecaptcha();
            ClickBeginAdventure();    */       
        }

        public string GetEmailValidation()
        {
            seleniumHelpers.WaitForElement(emailValidation);
            return driver.FindElement(emailValidation).Text;
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

        public string GetTermsAndConditionValidation()
        {
            seleniumHelpers.WaitForElement(termsAndConditionValidation);
            return driver.FindElement(termsAndConditionValidation).Text;
        }

        public bool IsRecaptchaValidationDisplayed()
        {
            return seleniumHelpers.IsElementDisplayed(recaptchaValidation);
        }

        public bool IsDateOfBirthValidationDisplayed()
        {
            return seleniumHelpers.IsElementDisplayed(dateofbirthValidation);
        }

    }
}
