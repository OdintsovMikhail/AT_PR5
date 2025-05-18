using AventStack.ExtentReports;
using PageObjectPattern;
using Reqnroll;

namespace AT_PR5.Steps
{
    [Binding]
    public sealed class Hooks
    {
        private static string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
        private static string reportName = "BDD test suite report.html";
        private static ExtentTest test;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Reporter.Instance.SetUpReporter(reportPath, reportName);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Reporter.Instance.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            test = Reporter.Instance.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            test.CreateNode(scenarioContext.ScenarioInfo.Title);
            Logger.Instance.InitializeTest(test);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Logger.Instance.DisposeTest();
            Driver.Quit();
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            if (context.TestError != null)
            {
                string scenario = context.ScenarioInfo.Title;
                string step = context.StepContext.StepInfo.Text;

                Logger.Instance.Error($"Scenatio '{scenario}' failed on step '{step}'");
                ExceptionHandler.HandleTestFailure(context.TestError, test);
            }
        }
    }
}