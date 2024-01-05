using AventStack.ExtentReports;
using GTAutomation.PageObject.Login;
using NUnit.Framework;
using System;
using GTAutomation.Entities;
using GTAutomation.Framework.DriverManager;
using GTAutomation.Framework.Report;
using GTAutomation.PageObject.Common;

namespace GTAutomation.TestCase
{
    public class LoginTest : BaseDriver 
    {
        ExtentTest test = null;

        [Test]
        public void TC_Log_01_TestLoginWithValidCredential()
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Log_01: To verify user is able to login with valid data.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver, test);

                homePage.ClickLogin();
                test.Log(Status.Pass, "Click on login on Home page.");

                LoginPage loginPage = new LoginPage(webDriver, test);
                              
                loginPage.LoginWithCredential(NickName, Password);
                test.Log(Status.Pass, "Enter login details and click on login button on Login page.");
                test.Log(Status.Info, "<b><u>Data Entered: </u></b>");
                test.Log(Status.Info, "<b>NickName : </b>" + NickName);
                test.Log(Status.Info, "<b>Password : </b>" + Password);

                if (String.Equals(NickName, homePage.GetProfileName()))
                {
                    test.Log(Status.Pass, "" + NickName + " " + " user logged in successfully.");
                }
                else
                {
                    test.Log(Status.Fail, "" + NickName + " " + " user logged in successfully. <br>", GetScreenShot(webDriver));
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
        public void TC_Log_02_TestLoginWithBlankDetails()
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Log_02: To verify user is not able to login with blank details.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver, test);

                homePage.ClickLogin();
                test.Log(Status.Pass, "Click on login on Home page.");

                LoginPage loginPage = new LoginPage(webDriver, test);

                loginPage.ClickLogin();
                test.Log(Status.Pass, "Click on login on Login page.");

                if (String.Equals(Message.NickNameValidation, loginPage.GetNickNameValidation()))
                {
                    test.Log(Status.Pass, "" + Message.NickNameValidation + " " + "validation message appear on Login page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.NickNameValidation + " " + "validation message appear on Login page. <br>", GetScreenShot(webDriver));
                }

                if (String.Equals(Message.PasswordValidation, loginPage.GetPasswordValidation()))
                {
                    test.Log(Status.Pass, "" + Message.PasswordValidation + " " + "validation message appear on Login page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.PasswordValidation + " " + "validation message appear on Login page. <br>", GetScreenShot(webDriver));
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
        public void TC_Log_03_TestLoginWithInvalidNickName()
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Log_03: To verify user is not able to login with invalid nickname.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver, test);

                homePage.ClickLogin();
                test.Log(Status.Pass, "Click on login on Home page.");

                LoginPage loginPage = new LoginPage(webDriver, test);

                User user = User.GetDetails;

                loginPage.LoginWithCredential(user.NickName, Password);
                test.Log(Status.Pass, "Enter invalid nick name and click login button on Login page .");
                test.Log(Status.Info, "<b><u>Data Entered: </u></b>");
                test.Log(Status.Info, "<b>Nick Name: </b>" + user.NickName);
                test.Log(Status.Info, "<b>Password: </b>" + Password);

                if (String.Equals(Message.InvalidNickNamePasswordValidation, loginPage.GetIncorrectNickNamePasswordValidation()))
                {
                    test.Log(Status.Pass, "" + Message.InvalidNickNamePasswordValidation + " " + "validation message appear on Login page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.InvalidNickNamePasswordValidation + " " + "validation message appear on Login page. <br>", GetScreenShot(webDriver));
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
        public void TC_Log_04_TestLoginWithInvalidPassword()
        {
            try
            {
                test = ExtentTestManager.CreateTest("TC_Log_04: To verify user is not able to login with invalid password.");
                webDriver = InitializeWebDriver(test);

                HomePage homePage = new HomePage(webDriver, test);

                homePage.ClickLogin();
                test.Log(Status.Pass, "Click on login on Home page.");

                LoginPage loginPage = new LoginPage(webDriver, test);

                User user = User.GetDetails;

                loginPage.LoginWithCredential(NickName, user.Password);
                test.Log(Status.Pass, "Enter invalid password and click login button on Login page.");
                test.Log(Status.Info, "<b><u>Data Entered: </u></b>");
                test.Log(Status.Info, "<b>Nick Name: </b>" + NickName);
                test.Log(Status.Info, "<b>Password: </b>" + user.Password);

                if (String.Equals(Message.InvalidNickNamePasswordValidation, loginPage.GetIncorrectNickNamePasswordValidation()))
                {
                    test.Log(Status.Pass, "" + Message.InvalidNickNamePasswordValidation + " " + "validation message appear on Login page.");
                }
                else
                {
                    test.Log(Status.Fail, "" + Message.InvalidNickNamePasswordValidation + " " + "validation message appear on Login page. <br>", GetScreenShot(webDriver));
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
             
    }
}
