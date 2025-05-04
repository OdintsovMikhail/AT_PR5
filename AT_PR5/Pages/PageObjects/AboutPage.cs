using OpenQA.Selenium;

namespace AT_PR5.Pages.PageObjects
{
    public class AboutPage : BasePage
    {
        By header = By.TagName("h1");

        public AboutPage()
        {
        }

        public string GetPageHeader()
        {
            return driver.FindElement(header).Text;
        }
    }
}
