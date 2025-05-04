using AT_PR5;
using AT_PR5.Pages.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using AT_PR5.ContactsData;

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
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifyNavigationToAboutPage));
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifyNavigationToAboutPage));
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

                Logger.Instance.Info("Searching using naviagation bar");
                var searchPage = homePage.Search(searchText);

                Logger.Instance.Info("Cheking search result");
                Assert.That(searchPage.GetUrl(), Does.Contain("/?s=study+programs"), "Search page url does not match");
            }
            catch (AssertionException ex)
            {
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifySearch));
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifySearch));
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
            Logger.Instance.InitializeTest(nameof(VerifyLanguageChange));

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
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifyLanguageChange));
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifyLanguageChange));
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
            Logger.Instance.InitializeTest(nameof(VerifyContactForm));

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
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifyContactForm));
                throw;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleTestFailure(ex, nameof(VerifyContactForm));
                throw;
            }
            finally
            {
                Logger.Instance.DisposeTest();
            }
        }
    }
}
