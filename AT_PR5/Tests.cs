using AT_PR5;
using AT_PR5.Pages.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using AT_PR5.ContactsData;
using AventStack.ExtentReports;
using Reqnroll.CommonModels;
using NUnit.Framework.Interfaces;

namespace PageObjectPattern
{
    [TestFixture]
    public class Tests
    {
        private static string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
        private static string reportName = "Test suite report.html";
        private ExtentTest test;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.Instance.Debug("Test suite set up successful");

            Reporter.Instance.SetUpReporter(reportPath, reportName);
        }

        [SetUp]
        public void SetUp()
        {
            Logger.Instance.Debug("Set up successful");

            test = Reporter.Instance.CreateTest(TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;

                switch (status)
                {
                    case TestStatus.Passed:
                        test.Pass("Test passed");
                        break;
                    case TestStatus.Skipped:
                        test.Skip("Test skipped");
                        break;
                }

                    Driver.Quit();
                Reporter.Instance.Flush();

                Logger.Instance.Debug("Teardown successful");
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Teardown error", ex);
            }
        }

        [Test]
        [Category("Navigation")]
        [TestCase("https://en.ehu.lt/", "https://en.ehu.lt/about/", "About", "About1", "About1")]
        public void VerifyNavigationToAboutPage(string url, string expectedUrl, string linkText, string expectedTitle, string expectedHeader)
        {
            Logger.Instance.InitializeTest(test);

            try
            {
                Logger.Instance.Info($"Navigating to {url}");
                Driver.Instance.Navigate().GoToUrl(url);
                var homePage = new HomePage();

                Logger.Instance.Info("Navigating to about page");
                var aboutPage = homePage.NavigateToAboutPage();

                Logger.Instance.Info("Checking URL");
                ClassicAssert.AreEqual(expectedUrl, aboutPage.GetUrl(), "URL missmach");
                Logger.Instance.Info("Checking page header");
                ClassicAssert.AreEqual(expectedTitle, aboutPage.GetTitle(), "Page header missmach");
                Logger.Instance.Info("Checking content header");
                ClassicAssert.AreEqual(expectedHeader, aboutPage.GetPageHeader(), "Content header missmach");
            }
            catch (AssertionException ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            finally
            {
                Logger.Instance.DisposeTest();
            }
        }

        [Test]
        [Category("Search")]
        [TestCase("https://en.ehu.lt/", "study programs")]
        public void VerifySearch(string url, string searchText)
        {
            Assert.Ignore();

            /*Logger.Instance.InitializeTest(test);
            
            try
            {
                Logger.Instance.Info($"Navigating to {url}");
                Driver.Instance.Navigate().GoToUrl(url);

                var homePage = new HomePage();

                Logger.Instance.Info("Searching using naviagation bar");
                var searchPage = homePage.Search(searchText);

                Logger.Instance.Info("Cheking search result");
                Assert.That(searchPage.GetUrl(), Does.Contain("/?s=study+programs"), "Search page url does not match");
            }
            catch (AssertionException ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            finally
            {
                Logger.Instance.DisposeTest();
            }*/
        }

        [Test]
        [Category("User accessibility")]
        [TestCase("https://en.ehu.lt/", "https://lt.ehu.lt/", "lt")]
        public void VerifyLanguageChange(string url, string expectedUrl, string languageToCheck)
        {
            Logger.Instance.InitializeTest(test);

            try
            {
                Logger.Instance.Info($"Navigating to {url}");
                Driver.Instance.Navigate().GoToUrl(url);

                Logger.Instance.Info($"Swiching language to {languageToCheck}");
                var homePage = new HomePage();
                homePage.SwitchLanguage(languageToCheck);

                Logger.Instance.Info("Cheking page language by URL");
                ClassicAssert.AreEqual(expectedUrl, homePage.GetUrl(), "URL missmach");
            }
            catch (AssertionException ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            finally
            {
                Logger.Instance.DisposeTest();
            }
        }

        [Test]
        [Category("Data accuracy")]
        [TestCase("https://en.ehu.lt/contact/", "franciskscarynacr@gmail.com", new string[] { "+370 68 771365", "+375 29 5781488" }, new string[] { "Facebook", "Telegram", "VK" })]
        public void VerifyContactForm(string url, string email, IEnumerable<string> phones, IEnumerable<string> socials)
        {
            Logger.Instance.InitializeTest(test);

            try
            {
                Logger.Instance.Info("Building contacts data");
                var expectedContacts = new ContactDataBuilder()
                    .WithEmail(email)
                    .WithPhones(phones)
                    .WithSocials(socials)
                    .Build();

                Logger.Instance.Info($"Navigating to {url}");
                Driver.Instance.Navigate().GoToUrl(url);

                Logger.Instance.Info("Getting contact info from the page");
                var contactPage = new ContactPage();
                var contacts = contactPage.GetContactData();

                Logger.Instance.Info("Matching contacts data with contact info from the page");
                Assert.That(expectedContacts.Equals(contacts), Is.True, "Contact info on page is incorrect");
            }
            catch (AssertionException ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, test);
                throw;
            }
            finally
            {
                Logger.Instance.DisposeTest();
            }
        }
    }
}
