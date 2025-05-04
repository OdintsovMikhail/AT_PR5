using AT_PR5.Pages.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_PR5.Pages
{
    public class PageFactory
    {
        public BasePage CreateStartPage(string pageName)
        {
            pageName = pageName.Trim().ToLower();

            return pageName switch
            {
                "home" => new HomePage(),
                "about" => new AboutPage(),
                "contact" => new ContactPage(),
                _ => throw new Exception($"There is no PageObject for page {pageName}")
            };
        }
    }
}
