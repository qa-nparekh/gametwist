using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GTAutomation.Framework.Helpers
{
    public class SeleniumHelpers
    {

        IWebDriver driver;

        public SeleniumHelpers()
        {
        }

        public SeleniumHelpers(IWebDriver driver)
        {
            this.driver = driver;
        }

        /// <summary>
        /// To enter element value
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="value">Value to enter</param>
        public static void EnterText(IWebElement element , string value, int pauseTime=0)
        {
            element.Clear();
            StaticWait(pauseTime);
            element.SendKeys(value);
            StaticWait(pauseTime);

        }

        /// <summary>
        /// To get the text of an element
        /// </summary>
        /// <param name="element">Element</param>
        /// <returns>Element text</returns>
        public static string GetText(IWebElement element)
        {
            return element.Text;
        }

        /// <summary>
        /// To verifiy visibilty of element
        /// </summary>
        /// <param name="elementXpath">By</param>
        /// <returns>True/False</returns>
        public bool IsElementDisplayed(By elementXpath)
        {
            IWebElement element = null;

            try
            {
                element = driver.FindElement(elementXpath);
                return element.Displayed;
            }
            catch(Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// To verify checked of element
        /// </summary>
        /// <param name="elementXpath">Element</param>
        /// <returns>True/False</returns>
        public bool IsElementSelected(By elementXpath)
        {
            IWebElement element = null;

            try
            {
                element = driver.FindElement(elementXpath);
                return element.Selected;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Wait for given seconds
        /// </summary>
        /// <param name="timeout">Timeout in Seconds</param>
        public static void StaticWait(int timeout)
        {
            TimeSpan interval = new TimeSpan(0, 0, timeout);
            Thread.Sleep(interval);
        }

        /// <summary>
        /// To select dropdown value by visible text
        /// </summary>
        /// <param name="by">Locator Method</param>
        /// <param name="value">Value to select</param>
        public void SelectDropDownByText(By by , string value)
        {
            IWebElement element = driver.FindElement(by);
            SeleniumHelpers.StaticWait(2);
            var dropdown = new SelectElement(element);
            dropdown.SelectByText(value);
        }

        /// <summary>
        /// To select dropdown value by visible index
        /// </summary>
        /// <param name="by">Locator Method</param>
        /// <param name="value">Value to select</param>
        public void SelectDropDownByIndex(By by, int value)
        {
            IWebElement element = driver.FindElement(by);
            SeleniumHelpers.StaticWait(2);
            var dropdown = new SelectElement(element);
            dropdown.SelectByIndex(value);
        }

        /// <summary>
        /// To perform page scroll to an element using java script
        /// </summary>
        /// <param name="element">Element</param>
        public void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// To enter text using java script
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="value">Value to enter</param>
        public void EnterTextUsingJS(IWebElement element , string value)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].value='"+value+"';", element);
        }

        /// <summary>
        /// To get text using java script
        /// </summary>
        /// <param name="by">Element</param>
        /// <returns>Element Text</returns>
        public string GetTextUsingJS(By by)
        {
            return (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].innerHTML;", driver.FindElement(by));
        }

        /// <summary>
        /// To perfoem screen refresh
        /// </summary>
        public void PageRefresh()
        {
            driver.Navigate().Refresh();
            WaitForPageLoad();
        }

        /// <summary>
        /// To hover over an element
        /// </summary>
        /// <param name="element">Element</param>
        public void MoveToElement(IWebElement element)
        {
            new Actions(driver).MoveToElement(element).Build().Perform();
        }

        /// <summary>
        /// To hover over an element using x , y
        /// </summary>
        /// <param name="element">Element</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void MoveToElement(IWebElement element , int x , int y)
        {
            new Actions(driver).MoveToElement(element, x, y).Build().Perform();
        }

        /// <summary>
        /// To Click And Hold
        /// </summary>
        /// <param name="element">Element</param>
        public void MoveElementAfterClickAndHold(IWebElement element)
        {
            new Actions(driver).ClickAndHold(element).MoveToElement(element, 50, 0).Build().Perform();
        }

        /// <summary>
        /// To drag and drop element
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="target">Target</param>
        public void DragAndDrop(IWebElement source , IWebElement target)
        {
            new Actions(driver).DragAndDrop(source, target).Build().Perform();
        }

        /// <summary>
        /// To drag and drop element
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="xOffset">X</param>
        /// <param name="yOffset">Y</param>
        public void DragAndDropByOffset(IWebElement source , int xOffset , int yOffset)
        {
            new Actions(driver).DragAndDropToOffset(source, xOffset, yOffset).Build().Perform();
        }

        /// <summary>
        /// Wait for element
        /// </summary>
        /// <param name="by">element path</param>
        /// <param name="pauseTime">pause time</param>
        /// <returns></returns>
        public IWebElement WaitForElement(By by , int pauseTime = 45)
        {
            SetImplicitWait(0);
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(pauseTime));
            element = wait.Until(e => e.FindElement(by));
            wait.Until(ExpectedConditions.ElementExists(by));
            SetImplicitWait();
            WaitForPageLoad();
            return element;
        }

        /// <summary>
        /// Wait for process finish till page load
        /// </summary>
        /// <param name="pauseTime">pausetime</param>
        /// <returns>element</returns>
        public IWebElement WaitForProcessFinish(int pauseTime = 45)
        {
            StaticWait(2);
            SetImplicitWait();
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(pauseTime));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='kt-spinner kt-spinner--v2 kt-spinner--lg kt-spinner--brand']")));
            SetImplicitWait();
            WaitForPageLoad();
            return element;
        }

        /// <summary>
        /// Wait for file upload finish
        /// </summary>
        /// <param name="pauseTime">pause time</param>
        /// <returns>element</returns>
        public IWebElement WaitForFileUploadFinish(int pauseTime = 45)
        {
            StaticWait(2);
            SetImplicitWait(0);
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(pauseTime));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("")));
            SetImplicitWait();
            WaitForPageLoad();
            return element;
        }

        /// <summary>
        /// Wait for element hide 
        /// </summary>
        /// <param name="by">by</param>
        /// <param name="pauseTime">pause time</param>
        /// <returns>element</returns>
        public IWebElement WaitForElementHide(By by , int pauseTime = 45)
        {
            StaticWait(2);
            SetImplicitWait(0);
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(pauseTime));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            SetImplicitWait();
            WaitForPageLoad();
            return element;
        }

        /// <summary>
        /// wait for page load
        /// </summary>
        /// <param name="pauseTime">pause time</param>
        public void WaitForPageLoad(int pauseTime = 45)
        {
            StaticWait(2);
            SetImplicitWait(0);
            IWait<IWebDriver> pageWait = new WebDriverWait(driver, TimeSpan.FromSeconds(pauseTime));
            pageWait.Until(waitDriver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            SetImplicitWait();
        }

        /// <summary>
        /// set implicit wait
        /// </summary>
        /// <param name="timeout">timeout</param>
        public void SetImplicitWait(int timeout = 10)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);
        }

        /// <summary>
        /// Clear all cookies
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public IReadOnlyCollection<Cookie> GetAllCookies(IWebDriver driver)
        {
            return driver.Manage().Cookies.AllCookies;
        }

        /// <summary>
        /// To verify element is Enabled 
        /// </summary>
        /// <param name="elementXpath">By</param>
        /// <returns>True/False</returns>
        public bool IsElementEnabled(By elementXpath)
        {
            IWebElement element = null;

            try
            {
                element = driver.FindElement(elementXpath);
                return element.Enabled;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// switch to iframe
        /// </summary>
        /// <param name="elementXpath">element xpath</param>
        public void SwitchToIFrame(By elementXpath)
        {
            driver.SwitchTo().Frame(driver.FindElement(elementXpath));
        }

        /// <summary>
        /// switch to default content from iframe
        /// </summary>
        public void SwitchToDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
        }

        /// <summary>
        /// To enter text using java script
        /// </summary>
        /// <param name="element">Element</param>
        public void ClickOnElementUsingJS(IWebElement element)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }
    }
}