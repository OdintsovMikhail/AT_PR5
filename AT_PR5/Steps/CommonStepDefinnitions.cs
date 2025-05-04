using System;
using AT_PR5.Pages;
using PageObjectPattern;
using Reqnroll;

namespace AT_PR5
{
    [Binding]
    public class CommonStepDefinnitions
    {
        private readonly ScenarioContext context;
        private PageFactory pageFactory = new PageFactory();

        private string homeUrl = "https://en.ehu.lt/";

        public CommonStepDefinnitions(ScenarioContext context)
        {
            this.context = context;
        }

        [Given("user starts on the {string} page")]
        public void GivenUserOnThePage(string pageName)
        {
            var pageUrl = PageMap.Urls[pageName.ToLower().Trim()];

            Logger.Instance.Info($"Navigating to {pageUrl}");
            Driver.Instance.Navigate().GoToUrl(pageUrl);

            context["CurrentPage"] = pageFactory.CreateStartPage(pageName);
        }
    }
}
