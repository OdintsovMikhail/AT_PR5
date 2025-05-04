using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using PageObjectPattern;

namespace AT_PR5.Pages.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver driver = Driver.Instance;
        protected Actions actions;

        public BasePage()
        {
            actions = new Actions(driver);
        }

        public string GetUrl()
        {
            return driver.Url;
        }

        public string GetTitle()
        {
            return driver.Title;
        }
    }
}
