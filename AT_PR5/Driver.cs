using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectPattern
{
    public class Driver
    {
        private static IWebDriver _instance;

        private Driver() { }

        public static IWebDriver Instance
        {
            get
            {
                if (_instance is null)
                {
                    var options = new ChromeOptions();
                    options.AddArgument("--start-maximized");
                    _instance = new ChromeDriver(options);
                }
                return _instance;
            }
        }

        public static void Quit()
        {
            if (_instance != null)
            {
                _instance.Quit();
                _instance.Dispose();
                _instance = null;
            }
        }
    }
}
