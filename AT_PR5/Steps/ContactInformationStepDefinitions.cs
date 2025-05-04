using AT_PR5.ContactsData;
using AT_PR5.Pages.PageObjects;
using NUnit.Framework;
using Reqnroll;
using System.Text.Json;

namespace AT_PR5.Steps
{
    [Binding]
    public class ContactInformationStepDefinitions
    {
        private readonly ScenarioContext context;

        private string contactPageUrl = "https://en.ehu.lt/contact/";

        public ContactInformationStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [Then("contact information on the page matches following data:")]
        public void ThenContactInformationOnThePageAreCorrect(string contactsJson)
        {
            var contactPage = context["CurrentPage"] as ContactPage;

            var dto = JsonSerializer.Deserialize<ContactDataDto>(contactsJson);

            var contacts = new ContactDataBuilder()
                .WithEmail(dto.Email)
                .WithPhones(dto.Phones)
                .WithSocials(dto.Socials)
                .Build();

            Logger.Instance.Info("Getting contact info from the page");
            var pageContacts = contactPage.GetContactData();

            Logger.Instance.Info("Matching contacts data with contact info from the page");
            Assert.That(contacts.Equals(pageContacts), Is.True, "Contact info on page is incorrect");
        }
    }
}
