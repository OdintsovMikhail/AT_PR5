using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using PageObjectPattern;
using Serilog;
using Serilog.Sinks.File.Archive;
using System.IO.Compression;


namespace AT_PR5
{
    public class Reporter
    {
        private static Reporter _instance;
        private bool systemDataSet = false;
        private ExtentReports _extentReports;

        private Reporter()
        {
            _extentReports = new ExtentReports();
        }

        public static Reporter Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new Reporter();
                }

                return _instance;
            }
        }

        public void SetUpReporter(string reportPath, string reportName)
        {
            if (_instance is not null)
            {
                Directory.CreateDirectory(reportPath);

                var htmlReporter = new ExtentSparkReporter(Path.Combine(reportPath, reportName));
                htmlReporter.Config.DocumentTitle = reportName;
                htmlReporter.Config.ReportName = reportName;

                _extentReports.AttachReporter(htmlReporter);

                if (!systemDataSet)
                {
                    _extentReports.AddSystemInfo("OS", Environment.OSVersion.ToString());
                    _extentReports.AddSystemInfo(".NET", Environment.Version.ToString());
                    _instance.AddTimeStamp("Test start");
                }

                systemDataSet = true;
            }
        }

        public void AddTimeStamp(string name)
        {
            _extentReports.AddSystemInfo(name, DateTime.Now.ToString("HH:mm:ss"));
        }

        public ExtentTest CreateTest(string name)
        {
            return _extentReports.CreateTest(name);
        }

        public void Flush()
        {
            _extentReports.Flush();
        }
    }
}
