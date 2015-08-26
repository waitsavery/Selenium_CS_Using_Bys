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

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Core
{
    public class Element
    {
        BaseTestClass btc;
        public IWebDriver driver;
        public IWebElement element;
        public By by;
        private string[] locator;
        private string message;
        private int loopCounter;

        public Element() { }

        public BaseTestClass getBaseTestClass() { return this.btc; }
        public void setBaseTestClass(BaseTestClass btc) {
            this.btc = btc;
            setDriver(this.btc.getDriver());
        }

        public IWebElement getElement() { return this.element; }
        public void setElement(IWebElement element) { this.element = element; }

        public IWebDriver getDriver() { return this.driver; }
        public void setDriver(IWebDriver driver) { this.driver = driver; }

        public By getBy() { return this.by; }
        public void setBy(By by) {
            locator = by.ToString().Split(':');
            this.by = by;
            this.element = getDriver().FindElement(getBy());
        }

        private string getElementLocator() { 
            return getElementByHow() + " = " + getElementByUsing(); 
        }

        private string getElementByHow() { return locator[0]; }
        private string getElementByUsing() { return locator[1]; }

        public void set(By by, string text) {
            setBy(by);
            if(text != "" && text != null){
                clear();
                sendKeys(text);
            }
        }

        public void clear() {
            message = "{font}" + DateTime.Now.ToString() + " :: {status} :: Clearing any existing text from textbox @FindBy[ " + getElementLocator() +"]{/font}";
            try
            {
                getElement().Clear();
                message = message.Replace("{font}", Constants.strFontBeginPass).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "PASS");
                Console.WriteLine(message);
            }
            catch (Exception e)
            {
                message = message.Replace("{font}", Constants.strFontBeginFail).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "FAILED");
                Console.WriteLine(message);
                throw e;
            } 
        }

        public void sendKeys(string text) {
            message = "{font}" + DateTime.Now.ToString() + " :: {status} :: Sending text [" + text + "] to textbox @FindBy[ " + getElementLocator() +"]{/font}";
            try
            {
                getElement().SendKeys(text);
                message = message.Replace("{font}", Constants.strFontBeginPass).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "PASS");
                Console.WriteLine(message);
            }catch(Exception e){
                message = message.Replace("{font}", Constants.strFontBeginFail).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "FAILED");
                Console.WriteLine(message);
                throw e;
            } 
        }

        public void click(By by) { 
            setBy(by);
            message = "{font}" + DateTime.Now.ToString() + " :: {status} :: Clicking element @FindBy[ " + getElementLocator() +"]{/font}";
            try
            {
                getElement().Click();
                message = message.Replace("{font}", Constants.strFontBeginPass).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "PASS");
                Console.WriteLine(message);
            }
            catch (Exception e)
            {
                message = message.Replace("{font}", Constants.strFontBeginFail).Replace("{/font}", Constants.strFontEnd).Replace("{status}", "FAILED");
                Console.WriteLine(message);
                throw e;
            }
        }

        public Boolean syncVisible(By by) { 
            setBy(by);
            return getElement().Displayed;
        }

        public Boolean syncVisible(By by, int timeout) {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeout));
            return syncVisible(by);
        }

        public Boolean syncVisible(By by, int timeout, Boolean failOnFalse){
            message = Constants.strFontBeginFail + "The element @FindBy[ "+getElementLocator()+ "] was not visible after ["+timeout.ToString()+"] seconds." + Constants.strFontEnd;
            Boolean elementVisible = syncVisible(by, timeout);
            if(failOnFalse){
                Assert.True(elementVisible, message);
                return elementVisible;
            }
            else{
                return elementVisible;
            }
        }

        public Boolean syncHidden(By by) { 
            return !syncVisible(by);
        }

        public Boolean syncHidden(By by, int timeout) {
            return !syncVisible(by, timeout);
        }

        public Boolean syncHidden(By by, int timeout, Boolean failOnFalse)
        {
            message = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + "] was not hidden after [" + timeout.ToString() + "] seconds." + Constants.strFontEnd;
            Boolean elementHidden = !syncVisible(by, timeout);
            if (failOnFalse)
            {
                Assert.True(elementHidden, message);
            }

            return elementHidden;
        }

        public Boolean syncEnabled(By by){
            setBy(by);
            return getElement().Enabled;
        }

        public Boolean syncEnabled(By by, int timeout) {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeout));
            return syncEnabled(by);
        }

        public Boolean syncEnabled(By by, int timeout, Boolean failOnFalse) {
            message = Constants.strFontBeginFail + "The element @FindBy[ " + getElementLocator() + "] was not enabled after [" + timeout.ToString() + "] seconds." + Constants.strFontEnd;
            Boolean elementEnabled = syncEnabled(by, timeout);
            if(failOnFalse){
                Assert.True(elementEnabled, "");
            }
            return elementEnabled;
        }

        public Boolean syncDisabled(By by) { 
            return !syncEnabled(by); 
        }

        public Boolean syncDisabled(By by, int timeout) { ret}
    }
}
