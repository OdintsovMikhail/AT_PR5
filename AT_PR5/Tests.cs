using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using PageObjectPattern.ContactsData;
using PageObjectPattern.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [TearDown]
        public void Teardown()
        {
            Driver.Quit();
        }

        [Test]
        [Category("Navigation")]
        [TestCase("https://en.ehu.lt/", "https://en.ehu.lt/about/", "About", "About", "About")]
        public void VerifyNavigationToAboutPage(string url, string expectedUrl, string linkText, string expectedTitle, string expectedHeader)
        {
            Driver.Instance.Navigate().GoToUrl(url);

            var homePage = new HomePage();
            var aboutPage = homePage.NavigateToAboutPage();

            ClassicAssert.AreEqual(expectedUrl, aboutPage.GetUrl(), "URL страницы не соответствует ожидаемому");
            ClassicAssert.AreEqual(expectedTitle, aboutPage.GetTitle(), "Заголовок страницы не соответствует ожидаемому");
            ClassicAssert.AreEqual(expectedHeader, aboutPage.GetPageHeader(), "Заголовок контента не совпадает");
        }

        [Test]
        [Category("Search")]
        [TestCase("https://en.ehu.lt/", "study programs")]
        public void VerifySearch(string url, string searchText)
        {
            Driver.Instance.Navigate().GoToUrl(url);

            var homePage = new HomePage();
            var searchPage = homePage.Search(searchText);

            Assert.That(searchPage.GetUrl(), Does.Contain("/?s=study+programs"));
        }

        [Test]
        [Category("User accessibility")]
        [TestCase("https://en.ehu.lt/", "https://lt.ehu.lt/", "lt")]
        public void VerifyLanguageChange(string url, string expectedUrl, string languageToCheck)
        {
            Driver.Instance.Navigate().GoToUrl(url);

            var homePage = new HomePage();
            homePage.SwitchLanguage(languageToCheck);

            ClassicAssert.AreEqual(expectedUrl, homePage.GetUrl(), "URL страницы не соответствует ожидаемому");
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
    }
}
