using OpenQA.Selenium;

namespace PageObjectPattern.PageObjects
{
    public class HomePage : BasePage
    {
        private By aboutLink = By.LinkText("About");

        private By searchIcon = By.XPath("//div[@class='header-search']");
        private By searchBar = By.Name("s");

        private By languageDropdownMenu = By.ClassName("language-switcher");

        public AboutPage NavigateToAboutPage()
        {
            driver.FindElement(aboutLink).Click();

            return new AboutPage();
        }

        public SearchPage Search(string searchText)
        {
            actions.MoveToElement(driver.FindElement(searchIcon)).Perform();

            var search = driver.FindElement(searchBar);
            search.SendKeys(searchText);
            search.SendKeys(Keys.Enter);

            return new SearchPage();
        }

        public void SwitchLanguage(string language)
        {
            By languageOption = By.XPath($"//li//a[text()='{language}']");

            actions.MoveToElement(driver.FindElement(languageDropdownMenu)).Perform();
            driver.FindElement(languageOption).Click();
        }
    }
}
