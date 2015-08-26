using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Selenium_CS_Using_Bys.Orasi.Selenium.Tests;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Core
{
    public class Element
    {
        BaseTestClass btc;
        public IWebDriver driver;
        public IWebElement element;
        public By by;

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
            this.by = by;
            this.element = getDriver().FindElement(getBy());
        }

        public void set(By by, string text) {
            setBy(by);
            if(text != "" && text != null){
                clear();
                sendKeys(text);
            }
        }

        public void clear() {
            getElement().Clear(); 
        }

        public void sendKeys(string text) {
            getElement().SendKeys(text); 
        }

        public void click(By by) { 
            setBy(by);
            getElement().Click();
        }
    }
}
