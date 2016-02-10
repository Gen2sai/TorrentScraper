using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NHtmlUnit;
using NHtmlUnit.Html;

namespace TorrentScraper.Class_Library
{
    class HorribleSubs
    {
        public Dictionary<string, string> fetchAnimeList()
        {

            //ip and port points to fiddler for webClient.
            WebClient webClient = new WebClient(BrowserVersion.CHROME,"127.0.0.1", 8888)
            {
                JavaScriptEnabled = true,
                ThrowExceptionOnScriptError = false,
                ThrowExceptionOnFailingStatusCode = false,
                CssEnabled = false
            };

            webClient.WaitForBackgroundJavaScript(10000);

            HtmlPage tempPage = (HtmlPage) webClient.GetPage("http://horriblesubs.info/shows/");

            //wait for 5.25 seconds before reloading the page and fetch full page.
            Thread.Sleep(5250);

            webClient.Options.JavaScriptEnabled = false;
            HtmlPage Page = (HtmlPage)webClient.GetPage("http://horriblesubs.info/shows/");

            string htmlPage = Page.WebResponse.ContentAsString;

            Dictionary<string, string> animeDictionary = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(htmlPage))
            {
                string pattern = "<div class=\"ind-show linkful\"><a href=\"(?<url>.*?)\" title=\"(?<title>.*?)\">[\\s\\S].*?</a></div>";
                
                MatchCollection m = Regex.Matches(htmlPage, pattern);

                foreach (Match match in m)
                {
                    animeDictionary.Add(match.Groups["title"].Value, match.Groups["url"].Value);
                }
            }
            return animeDictionary;
        }
    }
}
