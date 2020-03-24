using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Selenium.Specflow.Core
{
    public class ExtentService
    {
        private static Dictionary<string, ExtentReports> dictionary = new Dictionary<string, ExtentReports>();
        [ThreadStatic]
        private static ExtentReports Instance;

        private static object _synclock = new object();

        public static string dirEvidencia = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\Debug\\Evidencias";
        public static string dirExtentConfigPipe = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\automacao\\extent-config.xml";
        public static string direExtentConfigLocal = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "\\Debug\\extent-config.xml";
        private readonly RemoteWebDriver driver;

        public ExtentService(RemoteWebDriver _driver)
        {
            this.driver = _driver;
        }

        public static ExtentReports initReport()
        {
            lock (_synclock)
            {

                var reporter = new ExtentHtmlReporter($@"{dirEvidencia}\{ExtentTestManager.FeatureName}\{ExtentTestManager.ScenarioName}\Execução_{ExtentTestManager.FeatureName}_{ExtentTestManager.ScenarioName.Substring(0, 10)}.html");
                //reporter.Configuration().Theme = Theme.Dark;
                //Instance.GherkinLanguage = "pt";
                var reports = new ExtentReports();
                if (File.Exists(dirExtentConfigPipe))
                    reporter.LoadConfig(dirExtentConfigPipe);
                else if (File.Exists(direExtentConfigLocal))
                    reporter.LoadConfig(direExtentConfigLocal);
                else
                    Assert.Fail("Erro ao carregar as configurações do relatorio");
                reports.AttachReporter(reporter);
                Instance = reports;
                dictionary.Add(ExtentTestManager.FeatureName + ExtentTestManager.ScenarioName, Instance);
                return reports;
            }
        }

        private static ExtentReports GetInstance()
        {
            if (dictionary.ContainsKey(ExtentTestManager.FeatureName + ExtentTestManager.ScenarioName))
            {
                var instance = dictionary[ExtentTestManager.FeatureName + ExtentTestManager.ScenarioName];
                return instance;
            }
            else
            {
                return null;
            }

        }

        public static void Flush()
        {
            try
            {
                var report = GetInstance();
                if (report != null)
                {
                    report.Flush();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }


    }
}
