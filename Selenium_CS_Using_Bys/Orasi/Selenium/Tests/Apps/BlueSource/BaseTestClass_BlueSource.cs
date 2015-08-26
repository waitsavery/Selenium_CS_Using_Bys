using NUnit.Framework;
using Selenium_CS_Using_Bys.Orasi.Selenium.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium_CS_Using_Bys.Orasi.Selenium.Tests.Apps.BlueSource
{
    public class BaseTestClass_BlueSource : BaseTestClass
    {
        public BaseTestClass_BlueSource()
        {
            Console.WriteLine("CLASS :: BaseTestClass_BlueSource");
        }
        [SetUp]
        public void setup()
        {
            setUrl(Constants.strEnvironment + "URL");
        }

        [TearDown]
        public void teardown()
        {
            getDriver().Quit();
            getDriver().Dispose();
        }
    }
}
