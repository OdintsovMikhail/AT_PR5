using AT_PR5;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using PageObjectPattern.ContactsData;
using PageObjectPattern.PageObjects;

namespace PageObjectPattern
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {
            Logger.Instance.Debug("Set up successful");
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                Driver.Quit();

                Logger.Instance.Debug("Teardown successful");
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Teardown error", ex);
            }
        }

        [Test]
        [Category("Navigation")]
        [TestCase("https://en.ehu.lt/", "https://en.ehu.lt/about/", "About", "About", "About")]
        public void VerifyNavigationToAboutPage(string url, string expectedUrl, string linkText, string expectedTitle, string expectedHeader)
        {
            Logger.Instance.InitializeTest(nameof(VerifyNavigationToAboutPage));

            try
            {
                Logger.Instance.Info($"Navigating to {url}");
                Driver.Instance.Navigate().GoToUrl(url);

                var homePage = new HomePage();
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
                HandleTestFailure(ex, nameof(VerifyNavigationToAboutPage));
                throw;
            }
            catch (Exception ex)
            {
                HandleTestFailure(ex, nameof(VerifyNavigationToAboutPage));
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
            Logger.Instance.InitializeTest(nameof(VerifySearch));

            try
            {
                Logger.Instance.Info($"Navigating to {url}");
                Driver.Instance.Navigate().GoToUrl(url);

                var homePage = new HomePage();
                var searchPage = homePage.Search(searchText);

                Logger.Instance.Info("Cheking search result");
                Assert.That(searchPage.GetUrl(), Does.Contain("/?s=study+programs"));
            }
            catch (AssertionException ex)
            {
                HandleTestFailure(ex, nameof(VerifyNavigationToAboutPage));
                throw;
            }
            catch (Exception ex)
            {
                HandleTestFailure(ex, nameof(VerifyNavigationToAboutPage));
                throw;
            }
            finally
            {
                Logger.Instance.DisposeTest();
            }
        }

        [Test]
        [Category("User accessibility")]
        [TestCase("https://en.ehu.lt/", "https://lt.ehu.lt/", "lt")]
        public void VerifyLanguageChange(string url, string expectedUrl, string languageToCheck)
        {
            Driver.Instance.Navigate().GoToUrl(url);

            var homePage = new HomePage();
            homePage.SwitchLanguage(languageToCheck);

            ClassicAssert.AreEqual(expectedUrl, homePage.GetUrl(), "URL missmach");
        }

        [Test]
        [Category("Navigation")]
        [TestCase("https://en.ehu.lt/contact/", "franciskscarynacr@gmail.com", new string[] { "+370 68 771365", "+375 29 5781488" }, new string[] { "Facebook", "Telegram", "VK" })]
        public void VerifyContactForm(string url, string email, IEnumerable<string> phones, IEnumerable<string> socials)
        {
            var expectedContacts = new ContactDataBuilder()
                .WithEmail(email)
                .WithPhones(phones)
                .WithSocials(socials)
                .Build();

            Driver.Instance.Navigate().GoToUrl(url);

            var contactPage = new ContactPage();
            var contacts = contactPage.GetContactData();

            Assert.That(expectedContacts.Equals(contacts), Is.True);
        }

        private void HandleTestFailure(Exception ex, string testName)
        {
            Logger.Instance.Error("Test failed with exception", ex);

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
