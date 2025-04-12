using OpenQA.Selenium;
using PageObjectPattern.ContactsData;

namespace PageObjectPattern.PageObjects
{
    public class ContactPage : BasePage
    {
        private By emailElement = By.XPath("//a[starts-with(@href, \"mailto:\")]");
        private By phoneLtElement = By.XPath("//li[strong[contains(text(), 'LT')]]");
        private By phoneByElement = By.XPath("//li[strong[contains(text(), 'BY')]]");
        private By socialsElement = By.XPath("//li[strong[contains(text(), 'social')]]//a");

        public ContactData GetContactData()
        {
            var elements = driver.FindElements(socialsElement);
            var socials = elements.Select(elem => elem.Text.Trim()).ToList();

            return new ContactDataBuilder()
                .WithEmail(driver.FindElement(emailElement).Text)
                .WithPhone(driver.FindElement(phoneLtElement).Text.Split(':')[1].Trim())
                .WithPhone(driver.FindElement(phoneByElement).Text.Split(':')[1].Trim())
                .WithSocials(socials)
                .Build();
        }
    }
}
