using System;
using AT_PR5.Pages.PageObjects;
using NUnit.Framework.Legacy;
using PageObjectPattern;
using Reqnroll;

namespace AT_PR5.Steps
{
    [Binding]
    public class NavigationToAboutPageStepDefinitions
    {
        private readonly ScenarioContext context;

        private string aboutPageUrl = "https://en.ehu.lt/about/";

        public NavigationToAboutPageStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [When("user clicks on {string} on navigation bar")]
        public void WhenUserClicksOnOnNavigationBar(string about)
        {
            var homePage = context["CurrentPage"] as HomePage;

            Logger.Instance.Info("Navigating to about page");
            context["CurrentPage"] = homePage.NavigateToAboutPage();
        }

        [Then("about page opens, containing {string} in page and content headers")]
        public void ThenAboutPageOpensContainingInPageAndContentHeaders(string about)
        {
            var aboutPage = context["CurrentPage"] as AboutPage;

            Logger.Instance.Info("Checking URL");
            ClassicAssert.AreEqual(aboutPageUrl, aboutPage.GetUrl(), "URL missmach");
            Logger.Instance.Info("Checking page header");
            ClassicAssert.AreEqual("About", aboutPage.GetTitle(), "Page header missmach");
            Logger.Instance.Info("Checking content header");
            ClassicAssert.AreEqual("About", aboutPage.GetPageHeader(), "Content header missmach");
        }
    }
}
