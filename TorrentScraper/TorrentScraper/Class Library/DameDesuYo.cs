using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NHtmlUnit;
using NHtmlUnit.Html;
using WebClient = NHtmlUnit.WebClient;

namespace TorrentScraper.Class_Library
{
    class DameDesuYo
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
            List<string> htmlStringList = new List<string>();

            htmlStringList.Add("http://damedesuyo.com/?page_id=1815");
            htmlStringList.Add("http://damedesuyo.com/?page_id=6757");
            htmlStringList.Add("http://damedesuyo.com/?page_id=2153");
            htmlStringList.Add("http://damedesuyo.com/?page_id=2151");

            List<Tuple<string, string>> tempAnimeList = new List<Tuple<string, string>>();
            Dictionary<string, string> animeDictionary = new Dictionary<string, string>();

            Parallel.ForEach(htmlStringList, htmlUrl =>
            {
                WebClient webClient = new WebClient(BrowserVersion.CHROME, "127.0.0.1", 8888)
                {
                    JavaScriptEnabled = false,
                    ThrowExceptionOnScriptError = false,
                    ThrowExceptionOnFailingStatusCode = false,
                    CssEnabled = false,
                };

                HtmlPage Page = (HtmlPage)webClient.GetPage(htmlUrl);

                string htmlPage = WebUtility.HtmlDecode(Page.WebResponse.ContentAsString);

                if (!String.IsNullOrEmpty(htmlPage))
                {
                    htmlPage = htmlPage.Replace(@"\r\n", "");
                    string pattern = "<div id=\"show\">(\\n|\\r|\\r\\n)<div id=\"staff\">(\\n|\\r|\\r\\n)<h2><a href=[\\s\\S].*><img class=[\\s\\S].* /></a><br />(\\n|\\r|\\r\\n)(?<title>.*?)</h2>(\\n|\\r|\\r\\n)<p><strong>Status:</strong> <a href=\".*?\">.*?</a>;(.*?)<a href=\"(?<url>.*?)\" target=\"_blank\">";

                    MatchCollection m = Regex.Matches(htmlPage, pattern, RegexOptions.Multiline);

                    foreach (Match match in m)
                    {
                        animeDictionary.Add(match.Groups["title"].Value, match.Groups["url"].Value);
                    }
                }
            });

            return animeDictionary;
        }

        public Dictionary<string, Dictionary<string, string>> GetEpisodes(string url)
        {
            //not a batch type
            HtmlPage page = (HtmlPage)webClient.GetPage(url);
            string htmlPage = WebUtility.HtmlDecode(page.WebResponse.ContentAsString);

            Dictionary<String, Dictionary<string, string>> EpisodeDictionary = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> LowRes = new Dictionary<string, string>();
            Dictionary<string, string> MidRes = new Dictionary<string, string>();
            Dictionary<string, string> HiRes = new Dictionary<string, string>();

            if (htmlPage.ToLower().Contains("www-download.png"))
            {
                HtmlPage tempPage = (HtmlPage)webClient.GetPage(page.Url.ToString() + "&showfiles=1");
                string tempDecodedPage = WebUtility.HtmlDecode(tempPage.WebResponse.ContentAsString);
                //batch processing
                string pattern = "<td class=\"viewtorrentname\">(?<AnimeTitle>.*?)</td>";

                Match match = new Regex(pattern).Match(htmlPage);

                if (tempDecodedPage.ToLower().Contains("x480"))
                {
                    //480p
                    LowRes.Add(match.Groups["AnimeTitle"].Value, page.Url.ToString().Replace(@"page=view&", @"page=download&"));
                }
                else if (tempDecodedPage.ToLower().Contains("x720"))
                {
                    //720p
                    MidRes.Add(match.Groups["AnimeTitle"].Value, page.Url.ToString().Replace(@"page=view&", @"page=download&"));
                }
                else if (tempDecodedPage.ToLower().Contains("x1080"))
                {
                    //1080p
                    HiRes.Add(match.Groups["AnimeTitle"].Value, page.Url.ToString().Replace(@"page=view&", @"page=download&"));
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(htmlPage))
                {
                    string pattern = "<tr class=\"trusted tlistrow\"><td class=\"tlisticon\"><a href=\"[\\s\\S].*?\"[\\s\\S].*?</a></td><td class=\"tlistname\"><a href=\"//(?<URL>.*?)\">(?<AnimeTitle>.*?)</a></td>";

                    MatchCollection m = Regex.Matches(htmlPage, pattern);

                    //not batch processing
                    foreach (Match match in m)
                    {
                        if (match.Groups["AnimeTitle"].Value.ToLower().Contains("x480"))
                        {
                            //480p
                            LowRes.Add(match.Groups["AnimeTitle"].Value, match.Groups["URL"].Value.Replace(@"page=view&", @"page=download&"));
                        }
                        else if (match.Groups["AnimeTitle"].Value.ToLower().Contains("x720"))
                        {
                            //720p
                            MidRes.Add(match.Groups["AnimeTitle"].Value, match.Groups["URL"].Value.Replace(@"page=view&", @"page=download&"));
                        }
                        else if (match.Groups["AnimeTitle"].Value.ToLower().Contains("1080"))
                        {
                            //1080p
                            HiRes.Add(match.Groups["AnimeTitle"].Value, match.Groups["URL"].Value.Replace(@"page=view&", @"page=download&"));
                        }
                        else if (match.Groups["AnimeTitle"].Value.ToLower().Contains("batch"))
                        {
                            //batch torrent but not download link
                            page = (HtmlPage)webClient.GetPage("http://" + match.Groups["URL"].Value.ToString() + "&showfiles=1");
                            htmlPage = WebUtility.HtmlDecode(page.WebResponse.ContentAsString);

                            string internalpattern = "<td class=\"viewtorrentname\">(?<AnimeTitle>.*?)</td>";

                            Match internalmatch = new Regex(internalpattern).Match(htmlPage);

                            if (htmlPage.ToLower().Contains("x480"))
                            {
                                //480p
                                LowRes.Add(internalmatch.Groups["AnimeTitle"].Value, page.Url.ToString().Replace(@"page=view&", @"page=download&"));
                            }
                            else if (htmlPage.ToLower().Contains("x720"))
                            {
                                //720p
                                MidRes.Add(internalmatch.Groups["AnimeTitle"].Value, page.Url.ToString().Replace(@"page=view&", @"page=download&"));
                            }
                            else if (htmlPage.ToLower().Contains("x1080"))
                            {
                                //1080p
                                HiRes.Add(internalmatch.Groups["AnimeTitle"].Value, page.Url.ToString().Replace(@"page=view&", @"page=download&"));
                            }
                        }
                    }
                }
            }

                if (LowRes.Count > 0)
                {
                    EpisodeDictionary.Add("LowRes", LowRes);
                }
                if (MidRes.Count > 0)
                {
                    EpisodeDictionary.Add("MidRes", MidRes);
                }
                if (HiRes.Count > 0)
                {
                    EpisodeDictionary.Add("HiRes", HiRes);
                }

            return EpisodeDictionary;
        }
    }
}
