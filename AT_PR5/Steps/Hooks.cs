using PageObjectPattern;
using Reqnroll;

namespace AT_PR5.Steps
{
    [Binding]
    public sealed class Hooks
    {

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            Logger.Instance.InitializeTest(scenarioContext.ScenarioInfo.Title);
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
                ExceptionHandler.HandleTestFailure(context.TestError, scenario);
            }
        }
    }
}