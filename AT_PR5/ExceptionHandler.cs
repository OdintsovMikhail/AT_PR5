using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_PR5
{
    public static class ExceptionHandler
    {
        public static void HandleTestFailure(Exception ex, string testName)
        {
            Logger.Instance.Error($"Test '{testName}' failed with exception", ex);

            try
            {
                var screenshotPath = Logger.Instance.TakeScreenshot(testName);
                Logger.Instance.Info($"Screenshot saved: {screenshotPath}");
            }
            catch (Exception screenshotEx)
            {
                Logger.Instance.Error("Failed to capture screenshot", screenshotEx);
            }
        }
    }
}
