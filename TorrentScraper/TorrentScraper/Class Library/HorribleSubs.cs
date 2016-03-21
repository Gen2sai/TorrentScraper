using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NHtmlUnit;
using NHtmlUnit.Html;

namespace TorrentScraper.Class_Library
{
    class HorribleSubs
    {
        //ip and port points to fiddler for webClient.
        WebClient webClient = new WebClient(BrowserVersion.CHROME, "127.0.0.1", 8888)
        {
            JavaScriptEnabled = false,
            ThrowExceptionOnScriptError = false,
            ThrowExceptionOnFailingStatusCode = false,
            CssEnabled = false
        };

        //Without running with fiddler.
        //WebClient webClient = new WebClient(BrowserVersion.CHROME)
        //{
        //    JavaScriptEnabled = false,
        //    ThrowExceptionOnScriptError = false,
        //    ThrowExceptionOnFailingStatusCode = false,
        //    CssEnabled = false,
        //};

        public Dictionary<string, string> fetchAnimeList()
        {
            //there is no more cloudflare
            //webClient.WaitForBackgroundJavaScript(10000);

            //HtmlPage tempPage = (HtmlPage)webClient.GetPage("http://horriblesubs.info/shows/");

            ////wait for 5.25 seconds before reloading the page and fetch full page.
            //Thread.Sleep(5250);

            //webClient.Options.JavaScriptEnabled = false;
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

        public Dictionary<string, Dictionary<string, string>> GetEpisodes(string url)
        {
            string showID = string.Empty;
            
            //extract the showID, another alternative is mapping all the showID to a database dictionary, which inturn only update value when changed.
            showID = extractShowID(url);

            //append http://horriblesubs.info/lib/getshows.php?type=show&showid=284
            url = "http://horriblesubs.info/lib/getshows.php?type=show&showid=" + showID;
            HtmlPage Page = (HtmlPage)webClient.GetPage(url);
            string htmlPage = Page.WebResponse.ContentAsString;

            Dictionary<String, Dictionary<string, string>> EpisodeDictionary = new Dictionary<string, Dictionary<string, string>>();

            if(!String.IsNullOrEmpty(htmlPage))
            {
                string pattern = "<td class=\"dl-label\"><i>(?<AnimeTitle>.*?)</i></td>.*?<td class=\"dl-type hs-magnet-link\"><span class=\"dl-link\"><a title=\"Magnet Link\" href=\"(?<MagnetLink>.*?)\">Magnet</a></span></td>";

                MatchCollection m = Regex.Matches(htmlPage, pattern);

                Dictionary<string, string> LowRes = new Dictionary<string, string>();
                Dictionary<string, string> MidRes = new Dictionary<string, string>();
                Dictionary<string, string> HiRes = new Dictionary<string, string>();

                foreach(Match match in m)
                {
                    if(match.Groups["AnimeTitle"].Value.ToLower().Contains("[480p]"))
                    {
                        //push 480p list
                        LowRes.Add(match.Groups["AnimeTitle"].Value, match.Groups["MagnetLink"].Value);
                    }
                    else if(match.Groups["AnimeTitle"].Value.ToLower().Contains("[720p]"))
                    {
                        //push 720p list
                        MidRes.Add(match.Groups["AnimeTitle"].Value, match.Groups["MagnetLink"].Value);
                    }
                    else if(match.Groups["AnimeTitle"].Value.ToLower().Contains("[1080p]"))
                    {
                        //push 1080p list
                        HiRes.Add(match.Groups["AnimeTitle"].Value, match.Groups["MagnetLink"].Value);
                    }
                }

                if(LowRes.Count > 0)
                {
                    EpisodeDictionary.Add("LowRes", LowRes);
                }
                if(MidRes.Count > 0)
                {
                    EpisodeDictionary.Add("MidRes", MidRes);
                }
                if(HiRes.Count > 0)
                {
                    EpisodeDictionary.Add("HiRes", HiRes);
                }
            }

            return EpisodeDictionary;
        }

        private string extractShowID(string url)
        {
            HtmlPage Page = (HtmlPage)webClient.GetPage(url);

            string htmlPage = Page.WebResponse.ContentAsString;

            string showID = string.Empty;
            if (!String.IsNullOrEmpty(htmlPage))
            {
                string pattern = "<script type=\"text/javascript\">var hs_showid = (?<showID>.*?) ;</script>";

                Match m = Regex.Match(htmlPage, pattern);

                showID = m.Groups["showID"].Value;
            }
            return showID;
        }
    }
}
