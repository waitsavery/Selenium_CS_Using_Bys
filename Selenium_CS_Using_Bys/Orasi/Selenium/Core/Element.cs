using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Selenium_CS_Using_Bys.Orasi.Selenium.Tests;
using NUnit.Framework;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Core
{
    public class Element
    {
        /*****************************************
         *             CLASS FIELDS              *
         *****************************************/ 
        BaseTestClass btc;
        public IWebDriver driver;
        public IWebElement element;
        public By by;
        private string[] arrLocator;
        private string strMessage;
        private int intLoopCounter;

        /*****************************************
         *           CLASS CONSTRUCTORS          *
         *****************************************/ 
        public Element() { }

        /*****************************************
         *           CLASS INTERACTIONS          *
         *****************************************/
        /// <summary>
        ///     Returns this instance of BaseTestClass
        /// </summary>
        /// <returns>Current instance of the BaseTestClass</returns>
        public BaseTestClass getBaseTestClass() { return this.btc; }
        /// <summary>
        ///     Sets the BaseTestClass
        /// </summary>
        /// <param name="btc">Current BaseTestClass</param>
        public void setBaseTestClass(BaseTestClass btc) {
            this.btc = btc;
            //Sets the local driver using the driver from the BaseTeestClass
            setDriver(this.btc.getDriver());
        }

        /*****************************************
         *           DRIVER INTERACTIONS         *
         *****************************************/
        /// <summary>
        ///     Returns the current IWebDriver
        /// </summary>
        /// <returns>Current IWebDriver</returns>
        public IWebDriver getDriver() { return this.driver; }
        /// <summary>
        ///     Sets the current IWebDriver
        /// </summary>
        /// <param name="driver">Current IWebDriver</param>
        public void setDriver(IWebDriver driver) { this.driver = driver; }

        /*****************************************
         *          ELEMENT INTERACTIONS         *
         *****************************************/
        /// <summary>
        ///     Returns the current element
        /// </summary>
        /// <returns>Current IWebElement</returns>
        public IWebElement getElement() { return this.element; }
        /// <summary>
        ///     Sets the current element
        /// </summary>
        /// <param name="element">IWebElement to set as the current element</param>
        public void setElement(IWebElement element) { this.element = element; }
        /// <summary>
        ///     Returns the current By locator
        /// </summary>
        /// <returns>Current By locator</returns>
        public By getBy() { return this.by; }
        /// <summary>
        ///     Defines the locator parts ("How" and "Using"), sets the current By locator and uses that By locator to set the current element
        /// </summary>
        /// <param name="by">Current By value</param>
        public void setBy(By by) {
            //Split the By locator into the "How" and "Using" parts
            arrLocator = by.ToString().Split(':');
            this.by = by;
            //Set the current element
            this.element = getDriver().FindElement(getBy());
        }
        /// <summary>
        ///     Returns the element locator "How" and "Using" as a usable string for use in reporting.
        ///     EX: "By.Id: username" where
        ///         "How" = By.Id
        ///         "Using" = username
        /// </summary>
        /// <returns>String element locator</returns>
        private string getElementLocator() { 
            return getElementByHow() + " = " + getElementByUsing(); 
        }
        /// <summary>
        ///     Returns the "How" part of the element locator
        /// </summary>
        /// <returns>String By-How</returns>
        private string getElementByHow() { return arrLocator[0]; }
        /// <summary>
        ///     Returns the "Using" part of the element locator
        /// </summary>
        /// <returns>Srting By-Using</returns>
        private string getElementByUsing() { return arrLocator[1]; }

        /*****************************************
         *          TEXTBOX INTERACTIONS         *
         *****************************************/
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement), clears any existing text and sends text to a textbox
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="text">String text to send</param>
        public void set(By by, string text) {
            setBy(by);
            //Skip this step if the string is empty or null
            if(text != "" && text != null){
                clear();
                sendKeys(text);
            }
            else { 
                Console.WriteLine("Value was blank; skipping this step."); 
            }
        }
        /// <summary>
        ///     Clears any existing text from a textbox
        /// </summary>
        public void clear() {
            //Define a base reporting message for this method
            strMessage = "{font}" + DateTime.Now.ToString() + " :: {status} :: Clearing any existing text from textbox @FindBy[ " + getElementLocator() + " ]{/font}";
            //Invoke the Selenium Clear() method, and report the resulting pass or fail
            try
            {
                getElement().Clear();
                strMessage = strMessage.Replace("{font}", Constants.strFontBeginPass).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "PASS");
                Console.WriteLine(strMessage);
            }
            catch (Exception e)
            {
                strMessage = strMessage.Replace("{font}", Constants.strFontBeginFail).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "FAILED");
                Console.WriteLine(strMessage);
                throw e;
            } 
        }
        /// <summary>
        ///     Sets the value of a textbox by simulating typing text into the element
        /// </summary>
        /// <param name="text"></param>
        public void sendKeys(string text){
            //Define a base reporting message for this method
            strMessage = "{font}" + DateTime.Now.ToString() + " :: {status} :: Sending text [ " + text + " ] to textbox @FindBy[ " + getElementLocator() + " ]{/font}";
            //Invoke the Selenium SendKeys() method, and report the resulting pass or fail
            try
            {
                getElement().SendKeys(text);
                strMessage = strMessage.Replace("{font}", Constants.strFontBeginPass).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "PASS");
                Console.WriteLine(strMessage);
            }catch(Exception e){
                strMessage = strMessage.Replace("{font}", Constants.strFontBeginFail).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "FAILED");
                Console.WriteLine(strMessage);
                throw e;
            } 
        }

        /*****************************************
         *           CLICK INTERACTIONS          *
         *****************************************/
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and invokes the Selenium Click() method 
        /// </summary>
        /// <param name="by">Current By value</param>
        public void click(By by) { 
            setBy(by);
            //Define a base reporting message for this method
            strMessage = "{font}" + DateTime.Now.ToString() + " :: {status} :: Clicking element @FindBy[ " + getElementLocator() + " ]{/font}";
            //Invoke the Selenium Click() method, and report the resulting pass or fail
            try
            {
                getElement().Click();
                strMessage = strMessage.Replace("{font}", Constants.strFontBeginPass).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "PASS");
                Console.WriteLine(strMessage);
            }
            catch (Exception e)
            {
                strMessage = strMessage.Replace("{font}", Constants.strFontBeginFail).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "FAILED");
                Console.WriteLine(strMessage);
                throw e;
            }
        }



        /*********************************************
         *********************************************
         *                   SYNCS                   *
         *********************************************
         *********************************************/
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Displayed" value to determine if the element is visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Displayed.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <returns>Boolean true if visible, false otherwise</returns>
        public Boolean syncVisible(By by) {
            Boolean elementVisible;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be visible.");
            try{
                elementVisible = getElement().Displayed;
            }
            catch (StaleElementReferenceException sere) { 
                Console.WriteLine("The element @FindBy[ "+getElementLocator()+" ] was found to be a stale element. Try reinitializing the elements for the current page.");
                throw sere;
            }
            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Displayed" value to determine if the element is visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Displayed.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <returns>Boolean true if visible, false otherwise</returns>
        public Boolean syncVisible(By by, int timeout) {
            Boolean elementVisible;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeout));
            elementVisible = syncVisible(by);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Constants.intImplicitWaitTimeout));
            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Displayed" value to determine if the element is visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Displayed.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <returns>Boolean true if visible, false otherwise</returns>
        public Boolean syncVisible(By by, Boolean failOnFalse)
        {
            Boolean elementVisible = syncVisible(by);
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not visible after [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds." + Constants.strFontEnd;
            if (failOnFalse && !elementVisible)
            {
                Assert.True(elementVisible, strMessage);
            }
            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Displayed" value to determine if the element is visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Displayed.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <returns>Boolean true if visible, false otherwise</returns>
        public Boolean syncVisible(By by, int timeout, Boolean failOnFalse){            
            Boolean elementVisible = syncVisible(by, timeout);
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not visible after [ " + timeout.ToString() + " ] seconds." + Constants.strFontEnd;
            if(failOnFalse && !elementVisible){
                Assert.True(elementVisible, strMessage);
            }
            return elementVisible;
        }
        /// <summary>
        ///     Repurposes the syncHidden() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <returns>Boolean true if hidden, false otherwise</returns>
        public Boolean syncHidden(By by) { 
            return !syncVisible(by);
        }
        /// <summary>
        ///     Repurposes the syncHidden() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <returns>Boolean true if hidden, false otherwise</returns>
        public Boolean syncHidden(By by, int timeout) {
            return !syncVisible(by, timeout);
        }
        /// <summary>
        ///     Repurposes the syncHidden() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not hidden, false if the test should continue</param>
        /// <returns>Boolean true if hidden, false otherwise</returns>
        public Boolean syncHidden(By by, Boolean failOnFalse)
        {
            Boolean elementVisible = !syncVisible(by);
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not hidden after [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds." + Constants.strFontEnd;
            if (failOnFalse && elementVisible)
            {
                Assert.False(elementVisible, strMessage);
            }
            return elementVisible;
        }
        /// <summary>
        ///     Repurposes the syncHidden() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not hidden, false if the test should continue</param>
        /// <returns>Boolean true if hidden, false otherwise</returns>
        public Boolean syncHidden(By by, int timeout, Boolean failOnFalse)
        {
            Boolean elementVisible = !syncVisible(by, timeout);
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not hidden after [ " + timeout.ToString() + " ] seconds." + Constants.strFontEnd;
            if (failOnFalse && elementVisible){
                Assert.False(elementVisible, strMessage);
            }
            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Enabled" value to determine if the element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Enabled.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <returns>Boolean true if enabled, false otherwise</returns>
        public Boolean syncEnabled(By by){
            Boolean elementEnabled;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be enabled.");
            try{
                elementEnabled = getElement().Enabled;
            }
            catch (StaleElementReferenceException sere) {
                Console.WriteLine("The element @FindBy[ " + getElementLocator() + " ] was found to be a stale element. Try reinitializing the elements for the current page.");
                throw sere;
            }
            return elementEnabled;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Enabled" value to determine if the element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Enabled.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <returns>Boolean true if enabled, false otherwise</returns>
        public Boolean syncEnabled(By by, int timeout) {
            Boolean elementEnabled;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeout));
            elementEnabled = syncEnabled(by);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Constants.intImplicitWaitTimeout));
            return elementEnabled;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Enabled" value to determine if the element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Enabled.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not enabled, false if the test should continue</param>
        /// <returns>Boolean true if enabled, false otherwise</returns>
        public Boolean syncEnabled(By by, Boolean failOnFalse)
        {
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not enabled after [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds." + Constants.strFontEnd;
            Boolean elementEnabled = syncEnabled(by);
            if (failOnFalse && !elementEnabled)
            {
                Assert.True(elementEnabled, strMessage);
            }
            return elementEnabled;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium "Enabled" value to determine if the element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/dotnet/html/P_OpenQA_Selenium_IWebElement_Enabled.htm
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not enabled, false if the test should continue</param>
        /// <returns>Boolean true if enabled, false otherwise</returns>
        public Boolean syncEnabled(By by, int timeout, Boolean failOnFalse) {
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not enabled after [ " + timeout.ToString() + " ] seconds." + Constants.strFontEnd;
            Boolean elementEnabled = syncEnabled(by, timeout);
            if (failOnFalse && !elementEnabled){
                Assert.True(elementEnabled, strMessage);
            }
            return elementEnabled;
        }
        /// <summary>
        ///     Repurposes the syncEnabled() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <returns>Boolean true if disabled, false otherwise</returns>
        public Boolean syncDisabled(By by) { 
            return !syncEnabled(by); 
        }
        /// <summary>
        ///     Repurposes the syncEnabled() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <returns>Boolean true if disabled, false otherwise</returns>
        public Boolean syncDisabled(By by, int timeout) { 
            return !syncEnabled(by, timeout);
        }
        /// <summary>
        ///     Repurposes the syncEnabled() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not disabled, false if the test should continue</param>
        /// <returns>Boolean true if disabled, false otherwise</returns>
        public Boolean syncDisabled(By by, Boolean failOnFalse)
        {
            Boolean elementEnabled = syncEnabled(by);
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not disabled after [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds." + Constants.strFontEnd;
            if (failOnFalse && elementEnabled)
            {
                Assert.False(elementEnabled, strMessage);
            }
            return elementEnabled;
        }
        /// <summary>
        ///     Repurposes the syncEnabled() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not disabled, false if the test should continue</param>
        /// <returns>Boolean true if disabled, false otherwise</returns>
        public Boolean syncDisabled(By by, int timeout, Boolean failOnFalse)
        {
            Boolean elementEnabled = syncEnabled(by, timeout);
            strMessage = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + " ] was not disabled after [ " + timeout.ToString() + " ] seconds." + Constants.strFontEnd;
            if (failOnFalse && elementEnabled)
            {
                Assert.False(elementEnabled, strMessage);
            }
            return elementEnabled;
        }

        /*********************************************
         *********************************************
         *         SYNCS WITH WEBDRIVERWAIT          *
         *********************************************
         *********************************************/
        /// <summary>
        ///     Used for reporting to determine if the element is expected to be visible or hidden
        /// </summary>
        /// <param name="expectedVisible">Boolean true is the element is expected to be visible, false otherwise</param>
        /// <returns>String value "visible" or "hidden"</returns>
        private string expectVisibleOrHidden(Boolean expectedVisible){
            if (expectedVisible)
                return "visible";
            else
                return "hidden";
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be visible false otherwise</param>
        /// <returns>Boolean true if the element is visible, false otherwise</returns>
        public Boolean syncVisibleWait(By by, Boolean expectedVisible){
            Boolean elementVisible = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectVisibleOrHidden(expectedVisible) + " within [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(Constants.intImplicitWaitTimeout));
            try{
                wait.Until(ExpectedConditions.ElementIsVisible(getBy()));
                elementVisible = true;
            }
            catch (TimeoutException){}
            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be visible false otherwise</param>
        /// <returns>Boolean true if the element is visible, false otherwise</returns>
        public Boolean syncVisibleWait(By by, int timeout, Boolean expectedVisible){ 
            Boolean elementVisible = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectVisibleOrHidden(expectedVisible) + " within [ " + timeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(timeout));
            try{
                wait.Until(ExpectedConditions.ElementIsVisible(getBy()));
                elementVisible = true;
            }
            catch (TimeoutException) { }

            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be visible false otherwise</param>
        /// <returns>Boolean true if the element is visible, false otherwise</returns>
        public Boolean syncVisibleWait(By by, Boolean failOnFalse, Boolean expectedVisible){
            Boolean elementVisible = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectVisibleOrHidden(expectedVisible) + " within [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(Constants.intImplicitWaitTimeout));
            try{
                wait.Until(ExpectedConditions.ElementIsVisible(getBy()));
                elementVisible = true;
            }
            catch (TimeoutException) { }

            if (failOnFalse && !elementVisible){
                Console.WriteLine(Constants.strFontBeginFail + DateTime.Now.ToString() + " :: FAIL :: " + "The element @FindBy[ " + getElementLocator() + " ] was not " + expectVisibleOrHidden(expectedVisible) + " after [" + Constants.intImplicitWaitTimeout + "] seconds." + Constants.strFontEnd);
                Assert.True(elementVisible);
            }
            return elementVisible;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become visible
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be visible false otherwise</param>
        /// <returns>Boolean true if the element is visible, false otherwise</returns>
        public Boolean syncVisibleWait(By by, int timeout, Boolean failOnFalse, Boolean expectedVisible){
            Boolean elementVisible = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectVisibleOrHidden(expectedVisible) + " within [ " + timeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(timeout));
            try{
                wait.Until(ExpectedConditions.ElementIsVisible(getBy()));
                elementVisible = true;
            }
            catch (TimeoutException) { }

            if (failOnFalse && !elementVisible){
                Console.WriteLine(Constants.strFontBeginFail + DateTime.Now.ToString() + " :: FAIL :: " + "The element @FindBy[ " + getElementLocator() + " ] was not " + expectVisibleOrHidden(expectedVisible) + " after [" + timeout.ToString() + "] seconds." + Constants.strFontEnd);
                Assert.True(elementVisible);
            }
            return elementVisible;
        }
        /// <summary>
        ///     Repurposes the syncVisibleWait() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be hidden, false otherwise</param>
        /// <returns>Boolean true if the element is expceted to be hidden, false otherwise</returns>
        public Boolean syncHiddenWait(By by, Boolean expectedVisible) { return !syncVisibleWait(by, expectedVisible); }
        /// <summary>
        ///     Repurposes the syncVisibleWait() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be hidden, false otherwise</param>
        /// <returns>Boolean true if the element is expceted to be hidden, false otherwise</returns>
        public Boolean syncHiddenWait(By by, int timeout, Boolean expectedVisible) { return !syncVisibleWait(by, timeout, expectedVisible); }
        /// <summary>
        ///     Repurposes the syncVisibleWait() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be hidden, false otherwise</param>
        /// <returns>Boolean true if the element is expceted to be hidden, false otherwise</returns>
        public Boolean syncHiddenWait(By by, Boolean failOnFalse, Boolean expectedVisible) { return !syncVisibleWait(by, failOnFalse, expectedVisible); }
        /// <summary>
        ///     Repurposes the syncVisibleWait() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be hidden, false otherwise</param>
        /// <returns>Boolean true if the element is expceted to be hidden, false otherwise</returns>
        public Boolean syncHiddenWait(By by, int timeout, Boolean failOnFalse, Boolean expectedVisible) { return !syncVisibleWait(by, timeout, failOnFalse, expectedVisible); }

        /// <summary>
        ///     Used for reporting to determine if the element is expected to be enabled or disabled
        /// </summary>
        /// <param name="expectedVisible">Boolean true is the element is expected to be enabled, false otherwise</param>
        /// <returns>String value "enabled" or "disabled"</returns>
        private string expectEnabledOrDisabled(Boolean expectedEnabled){
            if (expectedEnabled)
                return "enabled";
            else
                return "disabled";
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become clickable, which is the standard used to determine if an element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be enabled false otherwise</param>
        /// <returns>Boolean true if the element is enabled, false otherwise</returns>
        public Boolean syncEnabledWait(By by, Boolean expectedEnabled) {
            Boolean elementEnabled = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectEnabledOrDisabled(expectedEnabled) + " within [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(Constants.intImplicitWaitTimeout));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(getBy()));
                elementEnabled = true;
            }
            catch (TimeoutException) { }
            return elementEnabled;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become clickable, which is the standard used to determine if an element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be enabled false otherwise</param>
        /// <returns>Boolean true if the element is enabled, false otherwise</returns>
        public Boolean syncEnabledWait(By by, int timeout, Boolean expectedEnabled)
        {
            Boolean elementEnabled = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectEnabledOrDisabled(expectedEnabled) + " within [ " + timeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(timeout));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(getBy()));
                elementEnabled = true;
            }
            catch (TimeoutException) { }
            return elementEnabled;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become clickable, which is the standard used to determine if an element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be enabled false otherwise</param>
        /// <returns>Boolean true if the element is enabled, false otherwise</returns>
        public Boolean syncEnabledWait(By by, Boolean failOnFalse, Boolean expectedEnabled)
        {
            Boolean elementEnabled = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectEnabledOrDisabled(expectedEnabled) + " within [ " + Constants.intImplicitWaitTimeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(Constants.intImplicitWaitTimeout));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(getBy()));
                elementEnabled = true;
            }
            catch (TimeoutException) { }
            if (failOnFalse && !elementEnabled) {
                Console.WriteLine(Constants.strFontBeginFail + DateTime.Now.ToString() + " :: FAIL :: " + "The element @FindBy[ " + getElementLocator() + " ] was not " + expectEnabledOrDisabled(expectedEnabled) + " after [" + Constants.intImplicitWaitTimeout.ToString() + "] seconds." + Constants.strFontEnd); 
            }
            return elementEnabled;
        }
        /// <summary>
        ///     Sets the current By value (and subsequently the current IWebElement) and uses the Selenium WebDriverWait.Until to wait for the element to become clickable, which is the standard used to determine if an element is enabled
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/FluentWait.html#until(com.google.common.base.Predicate)
        ///     SEE: https://selenium.googlecode.com/svn/trunk/docs/api/java/org/openqa/selenium/support/ui/ExpectedConditions.html
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedVisible">Boolean true if the element is expected to be enabled false otherwise</param>
        /// <returns>Boolean true if the element is enabled, false otherwise</returns>
        public Boolean syncEnabledWait(By by, int timeout, Boolean failOnFalse, Boolean expectedEnabled)
        {
            Boolean elementEnabled = false;
            setBy(by);
            Console.WriteLine(DateTime.Now.ToString() + " :: INFO :: " + "Syncing to element @FindBy[ " + getElementLocator() + " ] to be " + expectEnabledOrDisabled(expectedEnabled) + " within [ " + timeout.ToString() + " ] seconds.");
            WebDriverWait wait = new WebDriverWait(getDriver(), TimeSpan.FromSeconds(timeout));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(getBy()));
                elementEnabled = true;
            }
            catch (TimeoutException) { }
            if (failOnFalse && !elementEnabled)
            {
                Console.WriteLine(Constants.strFontBeginFail + DateTime.Now.ToString() + " :: FAIL :: " + "The element @FindBy[ " + getElementLocator() + " ] was not " + expectEnabledOrDisabled(expectedEnabled) + " after [" + timeout.ToString() + "] seconds." + Constants.strFontEnd);
            }
            return elementEnabled;
        }
        /// <summary>
        ///     Repurposes the syncEnabledWait() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="expectedEnabled">Boolean true if the element is expected to be disabled, false otherwise</param>
        /// <returns>Boolean true if the element is disabled, false otherwise</returns>
        public Boolean syncDisabledWait(By by, Boolean expectedEnabled) { return !syncEnabledWait(by, expectedEnabled); }
        /// <summary>
        ///     Repurposes the syncEnabledWait() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="expectedEnabled">Boolean true if the element is expected to be disabled, false otherwise</param>
        /// <returns>Boolean true if the element is disabled, false otherwise</returns>
        public Boolean syncDisabledWait(By by, int timeout, Boolean expectedEnabled) { return !syncEnabledWait(by, timeout, expectedEnabled); }
        /// <summary>
        ///     Repurposes the syncEnabledWait() method to determine if an element is disabled
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not disabled, false if the test should continue</param>
        /// <param name="expectedEnabled">Boolean true if the element is expected to be enabled, false otherwise</param>
        /// <returns>Boolean true if the element is disabled, false otherwise</returns>
        public Boolean syncDisabledWait(By by, Boolean failOnFalse, Boolean expectedEnabled) { return !syncEnabledWait(by, failOnFalse, expectedEnabled); }
        /// <summary>
        ///     Repurposes the syncEnabledWait() method to determine if an element is hidden
        /// </summary>
        /// <param name="by">Current By value</param>
        /// <param name="timeout">Integer timeout to use while searching for the element</param>
        /// <param name="failOnFalse">Boolean true if the test is to fail if the element is not visible, false if the test should continue</param>
        /// <param name="expectedEnabled">Boolean true if the element is expected to be enabled, false otherwise</param>
        /// <returns>Boolean true if the element is disabled, false otherwise</returns>
        public Boolean syncDisabledWait(By by, int timeout, Boolean failOnFalse, Boolean expectedEnabled) { return !syncEnabledWait(by, timeout, failOnFalse, expectedEnabled); }
    }
}
