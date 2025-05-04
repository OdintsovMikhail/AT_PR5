using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT_PR5.Pages
{
    public static class PageMap
    {
        public static readonly Dictionary<string, string> Urls = new()
    {
        { "home", "https://en.ehu.lt/" },
        { "about", "https://en.ehu.lt/about/" },
        { "contact", "https://en.ehu.lt/contact/" }
    };
    }
}
