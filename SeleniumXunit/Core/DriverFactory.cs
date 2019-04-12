using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;


namespace SeleniumXunit.Core
{
    public class DriverFactory
    {
        private static IWebDriver _driver;

        private DriverFactory(){}

        public static IWebDriver GetDriver() {

            if (_driver == null)
            {
                switch (Property._browser)
                {
                    case Property.Browsers.Chrome:
                        _driver = new ChromeDriver("/Users/e-msoares/Projects/SeleniumDotNet/SeleniumXunit/Driver");
                        break;
                    case Property.Browsers.Firefox:
                        _driver = new FirefoxDriver();
                        break;
                }
            }

            return _driver;
        }

        public static void KillDriver() { 
        
            if(_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        
        }
    }
}
