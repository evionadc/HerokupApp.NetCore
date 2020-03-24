using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Selenium.Specflow.Core
{
    public class ExtentTestManager
    {

        private static Dictionary<string, ExtentTest> _featureMap = new Dictionary<string, ExtentTest>();
        [ThreadStatic]
        private static List<string> paths;
        [ThreadStatic]
        private static List<string> logs;

        private static ExtentTest _feature;
        [ThreadStatic]
        private static ExtentTest _scenario;
        [ThreadStatic]
        private static ExtentTest _stepTest;
        [ThreadStatic]
        public static string FeatureName;
        [ThreadStatic]
        public static string ScenarioName;

        private static readonly object _synclock = new object();
        private readonly IWebDriver driver;
        public ExtentTestManager(RemoteWebDriver _driver)
        {
            this.driver = _driver;
        }

        // creates a parent test
        public static ExtentTest CreateFeature(string featurename, string description = null)
        {
            lock (_synclock)
            {
                FeatureName = featurename;

                if (!_featureMap.ContainsKey(featurename + ScenarioName))
                {
                    _feature = ExtentService.initReport().CreateTest(new GherkinKeyword("Feature"), featurename, description);
                    _featureMap.Add(featurename + ScenarioName, _feature);
                    return _feature;
                }
                else
                {
                    _feature = _featureMap[featurename + ScenarioName];
                    return _feature;
                }
            }
        }

        // creates a node
        // node is added to the parent using the parentName
        // if the parent is not available, it will be created
        public static ExtentTest CreateScenario(string featurename, string cenarioname, string description = null)
        {
            lock (_synclock)
            {

                ScenarioName = cenarioname;
                _scenario = CreateFeature(featurename).CreateNode(new GherkinKeyword("Scenario"),
                    cenarioname, description);
                return _scenario;
            }
        }

        public static ExtentTest CreateStep(string cenarioname, string stepName, string gherkin, object result, ScenarioContext scenarioContext, string description = null)
        {
            lock (_synclock)
            {
                ExtentTest scenario = null;

                scenario = GetScenario();

                var stepType = gherkin;
                if (result.ToString() == "StepDefinitionPending")
                {
                    if (stepType == "Given")
                        _stepTest = scenario.CreateNode(new GherkinKeyword("Given"), stepName, description).Skip("Step Definition Pending");
                    else if (stepType == "When")
                        _stepTest = scenario.CreateNode(new GherkinKeyword("When"), stepName).Skip("Step Definition Pending");
                    else if (stepType == "Then")
                        _stepTest = scenario.CreateNode(new GherkinKeyword("Then"), stepName).Skip("Step Definition Pending");
                    else if (stepType == "And")
                        _stepTest = scenario.CreateNode(new GherkinKeyword("And"), stepName).Skip("Step Definition Pending");
                }
                else
                {
                    if (scenarioContext.TestError == null)
                    {
                        if (stepType == "Given")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("Given"), stepName);
                        else if (stepType == "When")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("When"), stepName);
                        else if (stepType == "Then")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("Then"), stepName);
                        else if (stepType == "And")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("And"), stepName);


                    }
                    else
                    {
                        if (stepType == "Given")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("Given"), stepName, description).Fail(scenarioContext.TestError.Message.ToString());
                        else if (stepType == "When")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("When"), stepName, description).Fail(scenarioContext.TestError.Message.ToString());
                        else if (stepType == "Then")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("Then"), stepName, description).Fail(scenarioContext.TestError.Message.ToString());
                        else if (stepType == "And")
                            _stepTest = scenario.CreateNode(new GherkinKeyword("And"), stepName, description).Fail(scenarioContext.TestError.Message.ToString());

                    }
                }
                return _stepTest;
            }
        }

        public static void InsertScreenCurrentStep(String path)
        {
            if (paths == null)
                paths = new List<string>();
            paths.Add(path);
        }

        public static void InsertLogCurrentStep(String log)
        {
            if (logs == null)
                logs = new List<string>();
            logs.Add(log);
        }

        public static void InsertPrints()
        {
            if (paths == null)
                return;
            if (paths.Count > 0)
            {
                foreach (var path in paths)
                {
                    GetStep().AddScreenCaptureFromPath(path);
                }
                paths.Clear();
            }

        }

        public static void InsertLogs()
        {
            if (logs == null)
                return;
            if (logs.Count > 0)
            {
                foreach (var log in logs)
                {
                    GetStep().Log(Status.Info, log);
                }
                logs.Clear();
            }

        }

        public static void InsertLogCurrentScenario(String log)
        {
            GetTest().Log(Status.Info, log);
        }

        public static ExtentTest GetScenario()
        {

            return _scenario;

        }

        public static ExtentTest GetTest()
        {

            return _feature;

        }

        public static ExtentTest GetStep()
        {

            return _stepTest;

        }
    }
}
