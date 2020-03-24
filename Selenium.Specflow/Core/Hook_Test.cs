using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Selenium.Specflow.Pages;
using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Selenium.Specflow.Core

{
    [Binding]
    public class Hook_Test
    {
        public static string screenshotsPasta { get; set; }
        private readonly FeatureContext featureContext;
        private readonly ScenarioContext scenarioContext;
        private static int inc { get; set; }
        private readonly IObjectContainer container;


        public Hook_Test(IObjectContainer _container, ScenarioContext _scenarioContext, FeatureContext _featureContext)
        {
            this.featureContext = _featureContext;
            this.scenarioContext = _scenarioContext;
            this.container = _container;
        }
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks


        [BeforeScenario]
        public void BeforeScenario()
        {

            string feature = featureContext.FeatureInfo.Title;
            string cenario = scenarioContext.ScenarioInfo.Title;
            var tags = scenarioContext.ScenarioInfo.Tags;
            DriverFactory.initDriver("Chrome").Url = "https://mark7.herokuapp.com";
            Inicializacao init = new Inicializacao(DriverFactory.GetDriver());
            init.Realizarlogin();
            ExtentTestManager.CreateScenario(feature, cenario, $@"-- Tags:{string.Join(",", tags)}");
            container.RegisterInstanceAs<RemoteWebDriver>(DriverFactory.GetDriver());


        }



        [AfterStep]
        public void insertReportingSteps()

        {

            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(scenarioContext, null);
            string cenario = scenarioContext.ScenarioInfo.Title;
            var currentstep = scenarioContext.StepContext.StepInfo.Text;

            ExtentTestManager.CreateStep(cenario, currentstep, stepType, TestResult, scenarioContext);
            hookImg();
            ExtentTestManager.InsertPrints();
            ExtentTestManager.InsertLogs();


        }

        [AfterScenario]
        public static void DisposeDriver()
        {

            DriverFactory.RemoveCurrentThread();
        }

        [AfterFeature]
        public static void DisposeExtentService()
        {
            ExtentService.Flush();

        }
        public static void WriteLog(string strLog)
        {
            ExtentTestManager.InsertLogCurrentStep(strLog);

        }

        public static void hookImg()
        {

            var print = CorePage.capturaImagem();
            ExtentTestManager.InsertScreenCurrentStep(print);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


    }

}
