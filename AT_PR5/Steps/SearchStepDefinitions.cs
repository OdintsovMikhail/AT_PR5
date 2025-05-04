using System;
using PageObjectPattern;
using Reqnroll;
using NUnit.Framework;
using AT_PR5.Pages.PageObjects;

namespace AT_PR5.Steps
{
    [Binding]
    public class SearchStepDefinitions
    {
        private readonly ScenarioContext context;

        private string resultUrl = "https://en.ehu.lt/about/";
        private string searchInput = "study programs";
        private string expectedUrlPart = "/?s=study+programs";

        public SearchStepDefinitions(ScenarioContext context)
        {
            this.context = context;
        }

        [When("user uses search on navigation bar to find {string}")]
        public void WhenUserUsesSearchOnNavigationBarToFind(string p0)
        {
            var homePage = context["CurrentPage"] as HomePage;

            Logger.Instance.Info("Searching using naviagation bar");
            context["CurrentPage"] = homePage.Search(searchInput);
        }

        [Then("search page opens with coresponding search results and url")]
        public void ThenSearchPageOpensWithCorespondingSearchResultsAndUrl()
        {
            var searchPage = context["CurrentPage"] as SearchPage;

            Logger.Instance.Info("Cheking search result");
            Assert.That(searchPage.GetUrl(), Does.Contain(expectedUrlPart), "Search page url does not match");
        }
    }
}
