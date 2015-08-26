using NUnit.Framework;
using Selenium_CS_Using_Bys.Orasi.Selenium.Pages.Apps.BlueSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Tests.Apps.BlueSource
{
    [TestFixture]
    public class Test_Login : BaseTestClass_BlueSource
    {
        [Test]
        public void test()
        {
            Page_LoginPage loginPage = new Page_LoginPage(getBaseTestClass());
            loginPage.login();
        }
    }
}
