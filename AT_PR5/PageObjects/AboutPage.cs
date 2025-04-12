using OpenQA.Selenium;

namespace PageObjectPattern.PageObjects
{
    public class AboutPage : BasePage
    {
        By header = By.TagName("h1");

        public string GetPageHeader()
        {
            return driver.FindElement(header).Text;
        }
    }
}
