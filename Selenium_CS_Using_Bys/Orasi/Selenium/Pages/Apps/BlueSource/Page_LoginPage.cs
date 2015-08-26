using NUnit.Framework;
using OpenQA.Selenium;
using Selenium_CS_Using_Bys.Orasi.Selenium.Core;
using Selenium_CS_Using_Bys.Orasi.Selenium.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Pages.Apps.BlueSource
{
    public class Page_LoginPage : BasePageClass_BlueSource
    {
        private By txtUserName = By.Id("employee_username");
        private By txtPassword = By.Id("employee_password");
        private By btnLogin = By.Name("commit");
        private By eleBlueSourceImage = By.Id("/html/body/header/div/div/a/img");
        private int loopCounter;

        public Page_LoginPage(BaseTestClass btc){
            setBaseTestClass(btc);
            verifyLoginPageURL();
        }

        public void login() { 
            enterUsername();
            enterPassword();
            clickLogin();
        }
        private void enterUsername() { 
            set(txtUserName, Constants.strUsername); 
        }
        private void enterPassword() {
            set(txtPassword, Constants.strPassword); 
        }
        private void clickLogin() { 
            click(btnLogin); 
        }
        private void verifyLoginPageURL() {
            loopCounter = 0;
            Boolean pageLoaded = false;
            do{
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                loopCounter++;
                if (getDriver().Url.Contains("/login")) { pageLoaded = true; }
                Assert.LessOrEqual(loopCounter, Constants.intTestTimeout * 10, "The expected login page URL was not found within [" + Constants.intTestTimeout.ToString() + "] seconds.");
            }while(!pageLoaded);
        }
    }
}
