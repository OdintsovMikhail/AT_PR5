using System;
using AT_PR5.Pages.PageObjects;
using NUnit.Framework.Legacy;
using Reqnroll;

namespace AT_PR5.Steps
{
    [Binding]
    public class LocalizationStepDefinitions
    {
        private readonly ScenarioContext context;

        private string languageToCheck = "lt";
        private string localizedUrl = "https://lt.ehu.lt/";

        public LocalizationStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [When("user changes language through drop down language list")]
        public void WhenUserChangesLanguageThroughDropDownLanguageList()
        {
            var homePage = context["CurrentPage"] as HomePage;

            Logger.Instance.Info($"Swiching language to {languageToCheck}");
            homePage.SwitchLanguage(languageToCheck);
        }

        [Then("opens version of main page on coresponding language with matchin URL")]
        public void ThenOpensVersionOfMainPageOnCorespondingLanguageWithMatchinURL()
        {
            var homePage = context["CurrentPage"] as HomePage;

            Logger.Instance.Info("Cheking page language by URL");
            ClassicAssert.AreEqual(localizedUrl, homePage.GetUrl(), "URL missmach");
        }
    }
}
