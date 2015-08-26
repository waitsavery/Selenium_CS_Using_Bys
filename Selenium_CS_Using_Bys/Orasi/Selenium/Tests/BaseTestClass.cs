using Selenium_CS_Using_Bys.Orasi.Selenium.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Tests
{
    public class BaseTestClass : WebDriver
    {
        private int intTestTimeout;

        public BaseTestClass()
        {
            setTestTimeout(Constants.intTestTimeout);
            setBaseTestClass(this);
        }

        public void setTestTimeout(int timeout)
        {
            this.intTestTimeout = timeout;
        }

        public int getTestTimeout()
        {
            return this.intTestTimeout;
        }
    }
}
