using AventStack.ExtentReports;
using GTAutomation.PageObject.Login;
using NUnit.Framework;
using System;
using GTAutomation.Entities;
using GTAutomation.Framework.DriverManager;
using GTAutomation.Framework.Report;
using GTAutomation.PageObject.Common;
using System.Collections.Generic;

namespace GTAutomation.TestCase
{
    public class RegistrationTest : BaseDriver
    {
        ExtentTest test = null;

        [Test]
        public void TC_Reg_01_TestCreateNewAccount()
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Reg_01: To verify user is able to create new account with valid data.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver,test);
                
                homePage.ClickRegister();
                test.Log(Status.Pass, "Click on register on Home page.");

                RegistrationPage registrationPage = new RegistrationPage(webDriver, test);

                User user = User.GetDetails;

                registrationPage.TypeAccountSetupDetails(user.Email, user.NickName, user.Password, user.Day,user.Month,user.Year);
                test.Log(Status.Pass, "Enter account setup details and click on begin adventure button on Registration page.");
                test.Log(Status.Info, "<b><u>Data Entered: </u></b>");
                test.Log(Status.Info, "<b>Email : </b>" + user.Email);
                test.Log(Status.Info, "<b>NickName : </b>" + user.NickName);
                test.Log(Status.Info, "<b>Password : </b>" + user.Password);
                test.Log(Status.Info, "<b>Date Of Birth : </b>" + user.Day + "/" + user.Month + "/" + user.Year);

            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, ex.Message + "<br>", GetScreenShot(webDriver));
                Console.WriteLine(ex);
                Assert.Fail();
            }
            finally
            {
                CloseBrowser(webDriver, test);
            }
        }

        [Test]
        public void TC_Reg_02_TestCreateAccountWithBlankDetails()
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Reg_02: To verify user is not able to create new account with blank details.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver,test);

                homePage.ClickRegister();
                test.Log(Status.Pass, "Click on register on Home page.");

                RegistrationPage registrationPage = new RegistrationPage(webDriver,test);

                User user = User.GetDetails;

                registrationPage.ClickBeginAdventure();
                test.Log(Status.Pass, "Click on begin adventure on Registration page.");

                if (String.Equals(Message.EmailValidation, registrationPage.GetEmailValidation()))
                {
                    test.Log(Status.Pass, "" + Message.EmailValidation + " " + "validation message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.EmailValidation + " " + "validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }

                if (String.Equals(Message.NickNameValidation, registrationPage.GetNickNameValidation()))
                {
                    test.Log(Status.Pass, "" + Message.NickNameValidation + " " + "validation message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.NickNameValidation + " " + "validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }

                if (String.Equals(Message.PasswordValidation, registrationPage.GetPasswordValidation()))
                {
                    test.Log(Status.Pass, "" + Message.PasswordValidation + " " + "validation message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.PasswordValidation + " " + "validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }

                if (registrationPage.IsDateOfBirthValidationDisplayed())
                {
                    test.Log(Status.Pass, "'Please select dayPlease select monthPlease select year' validtion message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "'Please select dayPlease select monthPlease select year' validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }

                if (registrationPage.IsRecaptchaValidationDisplayed())
                {
                    test.Log(Status.Pass, "'The security check is a required field. Please enter the code.' validtion message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "'The security check is a required field. Please enter the code.' validtion message appear on Registration page. <br>", GetScreenShot(webDriver));
                }

                if (String.Equals(Message.TermsAndConditionValidation, registrationPage.GetTermsAndConditionValidation()))
                {
                    test.Log(Status.Pass, "" + Message.TermsAndConditionValidation + " " + "validation message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.TermsAndConditionValidation + " " + "validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }

            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, ex.Message + "<br>", GetScreenShot(webDriver));
                Console.WriteLine(ex);
                Assert.Fail();
            }
            finally
            {
                CloseBrowser(webDriver, test);
            }
        }

        [Test]
        [TestCaseSource("InvalidEmailAddressFormat")]
        public void TC_Reg_03_TestCreateAccountWithInvalidEmailDetails(string invalidEmailAddressFormat)
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Reg_03:To verify user is not able to create new account with invalid email format.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver, test);

                homePage.ClickRegister();
                test.Log(Status.Pass, "Click on register on Home page.");

                RegistrationPage registrationPage = new RegistrationPage(webDriver, test);

                User user = User.GetDetails;

                registrationPage.TypeEmail(invalidEmailAddressFormat);
                test.Log(Status.Pass, "Enter invalid email address format on Registration page .");
                test.Log(Status.Info, "<b><u>Data Entered: </u></b>");
                test.Log(Status.Info, "<b>Email Address: </b>" + invalidEmailAddressFormat);

                if (String.Equals(Message.InValidEmailValidation, registrationPage.GetEmailValidation()))
                {
                    test.Log(Status.Pass, "" + Message.InValidEmailValidation + " " + "validation message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.InValidEmailValidation + " " + "validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, ex.Message + "<br>", GetScreenShot(webDriver));
                Console.WriteLine(ex);
                Assert.Fail();
            }
            finally
            {
                CloseBrowser(webDriver, test);
            }
        }
      
        [Test]
        [TestCaseSource("InvalidNickNameFormat")]
        public void TC_Reg_04_TestCreateAccountWithInvalidNickNameDetails(string invalidNickNameFormat,string invalidNickNameValidation )
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Reg_04:To verify user is not able to create new account with invalid nickname format.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver, test);

                homePage.ClickRegister();
                test.Log(Status.Pass, "Click on register on Home page.");

                RegistrationPage registrationPage = new RegistrationPage(webDriver, test);

                User user = User.GetDetails;

                registrationPage.TypeNickName(invalidNickNameFormat);
                test.Log(Status.Pass, "Enter invalid nick name format on Registration page.");
                test.Log(Status.Info, "<b><u>Data Entered: </u></b>");
                test.Log(Status.Info, "<b>Nick Name: </b>" + invalidNickNameFormat);

                if (String.Equals(invalidNickNameValidation, registrationPage.GetNickNameValidation()))
                {
                    test.Log(Status.Pass, "" + invalidNickNameValidation + " " + "validation message appear on Registration page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + invalidNickNameValidation + " " + "validation message appear on Registration page. <br>", GetScreenShot(webDriver));
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, ex.Message + "<br>", GetScreenShot(webDriver));
                Console.WriteLine(ex);
                Assert.Fail();
            }
            finally
            {
                CloseBrowser(webDriver, test);
            }
        }

        private static IEnumerable<TestCaseData> InvalidEmailAddressFormat()
        {
            yield return new TestCaseData("invalidemail@@gmail.com");
            yield return new TestCaseData("..we!#$%&'*/=?^_+-`{|}~|.@Gmail.com");
        }

        private static IEnumerable<TestCaseData> InvalidNickNameFormat()
        {
            yield return new TestCaseData("ab", Message.InValidNickNameCharacterValidation);
            yield return new TestCaseData("ab@%$^()", Message.InValidNickNameSymbolValidation);
        }
    }
}
