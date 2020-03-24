using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace Selenium.Specflow.Core
{
    public class DriverFactory
    {

        private static readonly object _synclock = new object();
        private static string pathchromedriver = @"C:\Users\miche\Source\Repos\evionadc\HerokupApp.NetCore\Selenium.Specflow\bin\Debug\netcoreapp3.0";
        private DriverFactory() { }

        public static RemoteWebDriver initDriver(string _browser)
        {

            lock (_synclock)
            {
                try
                {

                    var proxy = "proxygol.sede.gol.com:80";
                    _driver = null;
                    switch (_browser)
                    {
                        case "Firefox":
                            FirefoxOptions fx = new FirefoxOptions();
                            Proxy p = new Proxy();
                            p.HttpProxy = proxy;
                            p.SslProxy = proxy;
                            p.FtpProxy = proxy;
                            fx.Proxy = p;
                            fx.AddArgument("--width=1920");
                            fx.AddArgument("--height=1080");
                            _driver = new FirefoxDriver(fx);
                            break;
                        case "Chrome":
                            var options = new ChromeOptions();
                            options.AddArgument("--no-sandbox");
                            _driver = new ChromeDriver(pathchromedriver, options);
                            break;
                        default:
                            _driver = new ChromeDriver(pathchromedriver);
                            break;
                    }


                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                    _driver.Manage().Window.Maximize();
                    return _driver;


                }
                catch (Exception e)
                {
                    if (_driver != null)
                    {
                        _driver.Quit();
                        _driver.Dispose();
                    }
                    return _driver;

                }
            }

        }

        public static void RemoveCurrentThread()
        {
            if (_driver != null)
            {
                _driver.Close();
                _driver.Quit();
                _driver = null;

            }

        }
        public static RemoteWebDriver GetDriver()
        {
            return _driver;

        }

        public static void Screenshot(string screenshotsPasta)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
                ss.SaveAsFile(screenshotsPasta, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Assert.Fail("Não foi possível tirar o screenshot: " + e.ToString());
            }
        }

        [ThreadStatic]
        public static RemoteWebDriver _driver;
    }
}
