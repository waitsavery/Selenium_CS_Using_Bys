using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Selenium_CS_Using_Bys.Orasi.Selenium.Core;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;
using Selenium_CS_Using_Bys.Orasi.Selenium.Tests;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Core
{
    public class WebDriver{
        public BaseTestClass btc;
        public IWebDriver driver;
        /*******************
         * DRIVER TIMEOUTS *
         *******************/
        private int intScriptTimeout;
        private int intImplicitWaitTimeout;
        private int intPageLoadTimeout;
        /*******************
         * TEST PARAMETERS *
         *******************/
        private string strBrowserUnderTest;
        private string strBrowserVersion;
        private string strOperatingSystem;
        private string strRunLocation;
        private string strEnvironment;
        /**************
         * DRIVER URL *
         **************/
        private string strUrl;

        public WebDriver(){
            Console.WriteLine("CLASS :: WebDriver");
            setTestParameters();
            try{
                if(getRunLocation().ToLower() == "local"){
                    switch (getBrowserUnderTest().ToLower())
                    {
                        case "chrome":
                            setDriver(new ChromeDriver(@"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CS_Using_Bys\Selenium_CS_Using_Bys\Orasi\Selenium\Core\Drivers"));
                            break;
                        case "firefox":
                            setDriver(new FirefoxDriver());
                            break;
                        case "ie":
                        case "iexplore":
                            setDriver(new InternetExplorerDriver(@"C:\Users\temp\Documents\Visual Studio 2013\Projects\Selenium_CS_Using_Bys\Selenium_CS_Using_Bys\Orasi\Selenium\Core\Drivers"));
                            break;
                        default:
                            throw new Exception("The value for the test parameter browser under test [" + getBrowserUnderTest() + "] is not valid.");
                    }
                }
                else if (getRunLocation().ToLower() == "remote")
                {

                }else{
                    throw new Exception("The value for the test parameter run location [" + getRunLocation() + "] is not valid.");
                }
                manageWebDriver();
            }catch(Exception e){
                Console.WriteLine("A exception occurred while initializing the web driver.\nEXCEPTION STACK TRACE:\n" + e.StackTrace);
                throw e;
            }
        }

        public void setTestParameters(string browserUnderTest, string browserVersion, string operatingSystem, string runLocation, string environment)
        {
            setBrowserUnderTest(browserUnderTest);
            setBrowserVersion(browserVersion);
            setOperatingSystem(operatingSystem);
            setRunLocation(runLocation);
            setEnvironment(environment);
        }

        public void setTestParameters()
        {
            setBrowserUnderTest(Constants.strBrowserUnderTest);
            setBrowserVersion(Constants.strBrowserVersion);
            setOperatingSystem(Constants.strOperatingSystem);
            setRunLocation(Constants.strRunLocation);
            setEnvironment(Constants.strEnvironment);
        }

        public void setBrowserUnderTest(string browserUnderTest){
            this.strBrowserUnderTest = browserUnderTest;
        }

        public string getBrowserUnderTest(){
            return this.strBrowserUnderTest;
        }

        public void setBrowserVersion(string browserVersion){
            this.strBrowserVersion = browserVersion;
        }

        public string getBrowserVersion(){
            return this.strBrowserVersion;
        }

        public void setOperatingSystem(string operatingSystem){
            this.strOperatingSystem = operatingSystem;
        }

        public string getOperatingSystem(){
            return this.strOperatingSystem;
        }

        public void setRunLocation(string runLocation){
            this.strRunLocation = runLocation;
        }

        public string getRunLocation()
        {
            return this.strRunLocation;
        }

        public void setEnvironment(string environment){
            this.strEnvironment = environment;
        }

        public string getEnvironment()
        {
            return this.strEnvironment;
        }

        public IWebDriver getDriver(){
            return this.driver;
        }

        public void setDriver(IWebDriver driver){
            this.driver = driver;
        }

        public void manageWebDriver()
        {
            setInitialTimeouts();
            setInitialCookies();
            setInitialWindow();
        }

        public void setScriptTimeout(int timeout){
            this.intScriptTimeout = timeout;
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(getScriptTimeout()));
        }

        public int getScriptTimeout(){
            return this.intScriptTimeout;
        }

        public void setImplicitWaitTimeout(int timeout){
            this.intImplicitWaitTimeout = timeout;
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(getImplicitWaitTimeout()));
        }

        public int getImplicitWaitTimeout(){
            return this.intImplicitWaitTimeout;
        }

        public void setPageLoadTimeout(int timeout){
            this.intPageLoadTimeout = timeout;
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(getPageLoadTimeout()));
        }

        public int getPageLoadTimeout(){
            return this.intPageLoadTimeout;
        }

        public void setInitialTimeouts(){
            setImplicitWaitTimeout(Constants.intImplicitWaitTimeout);
            setPageLoadTimeout(Constants.intPageLoadTimeout);
            setScriptTimeout(Constants.intScriptTimeout);
        }

        public void setInitialCookies(){
            driver.Manage().Cookies.DeleteAllCookies();
        }

        public void setInitialWindow(){
            driver.Manage().Window.Maximize();
        }

        public void setUrl(string url)
        {
            switch (url)
            {
                case "stageURL":
                    this.strUrl = Constants.stageURL;
                    break;
                default:
                    break;
            }
            driver.Url = getUrl();
        }

        public string getUrl()
        {
            return this.strUrl;
        }

        public BaseTestClass getBaseTestClass() { 
            return this.btc; 
        }

        public void setBaseTestClass(BaseTestClass btc) { 
            this.btc = btc; 
        }
    }
}