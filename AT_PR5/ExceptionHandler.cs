using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace AT_PR5
{
    public static class ExceptionHandler
    {
        public static void HandleTestFailure(Exception ex, ExtentTest test)
        {
            Logger.Instance.Error($"Test '{test.Test.Name}' failed with exception", ex);

            try
            {
                var screenshotPath = Logger.Instance.TakeScreenshot(test.Test.Name);
                test.AddScreenCaptureFromPath(screenshotPath);
                Logger.Instance.Info($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception screenshotEx)
            {
                Logger.Instance.Error("Failed to capture screenshot", screenshotEx);
            }
        }
    }
}
