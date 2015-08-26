using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Core
{
    public class Constants
    {
        public const int intScriptTimeout = 20;
        public const int intImplicitWaitTimeout = 20;
        public const int intPageLoadTimeout = 60;
        public const int intTestTimeout = 60;

        public const string strBrowserUnderTest = "chrome";
        public const string strBrowserVersion = "";
        public const string strOperatingSystem = "";
        public const string strRunLocation = "local";
        public const string strEnvironment = "stage";

        public const string stageURL = "https://bluesourcestaging.herokuapp.com";

        public const string strUsername = "company.admin";
        public const string strPassword = "1234";

        //public const string strFontBeginPass = "<font size = 2 color=\"green\">";
        //public const string strFontBeginFail = "<font size = 2 color=\"red\">";
        //public const string strFontEnd = "</font>";
        public const string strFontBeginPass = "";
        public const string strFontBeginFail = "";
        public const string strFontEnd = "";
    }
}
