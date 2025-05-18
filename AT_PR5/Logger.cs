using AventStack.ExtentReports;
using OpenQA.Selenium;
using PageObjectPattern;
using Serilog;
using Serilog.Sinks.File.Archive;
using System.IO.Compression;


namespace AT_PR5
{
    public class Logger
    {
        private static Logger _instance;
        private ILogger _logger;
        private ExtentTest? _report;

        private ILogger basicLogger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: "Logs/BaseLog.txt",
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10 * 1024 * 1024, //10мб
                    rollOnFileSizeLimit: true,
                    hooks: new ArchiveHooks(CompressionLevel.Fastest, "Logs/Archive"))
                .CreateLogger();

        private Logger()
        {
            _logger = basicLogger;
        }

        public static Logger Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }

        public void InitializeTest(ExtentTest test)
        {
            string testName = test.Test.Name;
            var testRunId = Guid.NewGuid();
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var logFileName = $"logs/{testName}_{timestamp}_{testRunId}.log";

            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: logFileName, 
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    fileSizeLimitBytes: 10 * 1024 * 1024, //10мб
                    rollOnFileSizeLimit: true,
                    hooks: new ArchiveHooks(CompressionLevel.Fastest, "Logs/Archive"))
                .CreateLogger();

            Info($"[START] Test: {testName}");
            Debug($"Start date: {timestamp}");
            Debug($"Run ID: {testRunId}");
            Debug($"Evironment: OS={Environment.OSVersion}, User={Environment.UserName}");

            _logger.Debug($"Logger initialized for test {testName}, ID: {testRunId}", testName, testRunId);

            _report = test;
        }

        public void DisposeTest()
        {
            _logger.Debug("Closing logging for current test");
            Log.CloseAndFlush();

            ArchiveAndCleanup("Logs", "Logs/Archive", maxFilesToKeep: 10);

            _logger = basicLogger;
            _report = null;
        }

        public void Info(string message)
        {
            _logger.Information(message);
            if (_report is not null)
            {
                _report.Info(message);
            }
        }

        public void Error(string message, Exception ex = null)
        {
            _logger.Error(ex, message);
            if (_report is not null)
            {
                _report.Fail(message + ex.ToString());
            }
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
            if (_report is not null)
            {
                _report.Warning(message);
            }
        }

        public string TakeScreenshot(string testName)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var fileName = $"Screenshots/{testName}_{timestamp}.png";
            Directory.CreateDirectory("Screenshots");

            var screenshot = ((ITakesScreenshot)Driver.Instance).GetScreenshot();
            screenshot.SaveAsFile(fileName);

            return Path.GetFullPath(fileName);
        }

        private void ArchiveAndCleanup(string logsDir, string archiveDir, int maxFilesToKeep)
        {
            Directory.CreateDirectory(archiveDir);

            var logFiles = Directory.GetFiles(logsDir, "*.log")
                .Where(f => !f.Contains("BaseLog"))
                .OrderByDescending(File.GetCreationTime)
                .ToList();

            foreach (var logFile in logFiles.Skip(maxFilesToKeep))
            {
                var fileName = Path.GetFileName(logFile);
                var archivePath = Path.Combine(archiveDir, fileName + ".gz");

                using (var original = File.OpenRead(logFile))
                using (var compressed = File.Create(archivePath))
                using (var gzip = new GZipStream(compressed, CompressionMode.Compress))
                {
                    original.CopyTo(gzip);
                }

                File.Delete(logFile);
            }
        }
    }
}
