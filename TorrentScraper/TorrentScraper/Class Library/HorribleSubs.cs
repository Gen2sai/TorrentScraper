using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using NHtmlUnit;
using NHtmlUnit.Html;
using System.Threading;
using WebClient = NHtmlUnit.WebClient;
using WebRequest = System.Net.WebRequest;

namespace TorrentScraper.Class_Library
{
    class HorribleSubs
    {
        public Dictionary<string, string> fetchAnimeList()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://horriblesubs.info/shows/");
            string htmlPage;
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                htmlPage = reader.ReadToEnd();
            }

            Dictionary<string, string> animeDictionary = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(htmlPage))
            {
                string pattern = "<div class=\"ind-show\"><a href=\"(?<url>.*?)\" title=\"(?<title>.*?)\">[\\s\\S].*?</a></div>";

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
            //url = "http://horriblesubs.info/lib/getshows.php?type=show&showid=" + showID;

            Dictionary<String, Dictionary<string, string>> EpisodeDictionary = new Dictionary<string, Dictionary<string, string>>();


            //loop each page for episode until get DONE!
            for (int i = 0; i >= 0; i++)
            {
                //https://horriblesubs.info/shows/active-raid/
                //url = "http://horriblesubs.info//lib/getshows.php?type=show&showid=" + showID + "&nextid=" + i + "&_=" + TimeHelper.EpochConverter(DateTime.Now);
                url = "https://horriblesubs.info/api.php?method=getshows&type=show&showid=" + showID + "&nextid=" + i + "&_=" + TimeHelper.EpochConverter(DateTime.Now);

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Proxy = new WebProxy("127.0.0.1:8888");
                string htmlPage;
                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    htmlPage = reader.ReadToEnd();
                }

                //check if end of episode list
                if (htmlPage.Length == 4 && htmlPage.IndexOf("DONE") == 0)
                {
                    break;
                }
                else
                {
                    Dictionary<string, string> LowRes = new Dictionary<string, string>();
                    Dictionary<string, string> MidRes = new Dictionary<string, string>();
                    Dictionary<string, string> HiRes = new Dictionary<string, string>();

                    string linePattern = "(<div class=\"rls-info-container\" id=.*?</div></div></div>)";

                    MatchCollection m = Regex.Matches(htmlPage, linePattern);

                    foreach (Match line in m)
                    {
                        string lineSeriesPattern = "<div class=\"rls-info-container\" id.*?</span>\\s(?<AnimeTitle>.*?) <strong>(?<Episode>.*?)</strong>";
                        Match lineMatch = Regex.Match(line.Value, lineSeriesPattern);
                        string lineSeriesName = lineMatch.Groups["AnimeTitle"].Value + " - " + lineMatch.Groups["Episode"].Value;

                        string lineMagnetPattern = "<span class=\"rls-link-label\">(?<Resolution>.*?):</span><span class=\"dl-type hs-magnet-link\"><a title=\"Magnet Link\" href=\"(?<MagnetLink>.*?)\">Magnet</a>";
                        MatchCollection magnetCollection = Regex.Matches(line.Value, lineMagnetPattern);
                        foreach (Match magnetEntry in magnetCollection)
                        {
                            if (magnetEntry.Groups["Resolution"].Value.ToLower().Contains("480p"))
                            {
                                //push 480p list
                                LowRes.Add(lineSeriesName, magnetEntry.Groups["MagnetLink"].Value);
                            }
                            else if (magnetEntry.Groups["Resolution"].Value.ToLower().Contains("720p"))
                            {
                                //push 720p list
                                MidRes.Add(lineSeriesName, magnetEntry.Groups["MagnetLink"].Value);
                            }
                            else if (magnetEntry.Groups["Resolution"].Value.ToLower().Contains("1080p"))
                            {
                                //push 1080p list
                                HiRes.Add(lineSeriesName, magnetEntry.Groups["MagnetLink"].Value);
                            }
                        }
                    }

                    if (LowRes.Count > 0)
                    {
                        if (EpisodeDictionary.ContainsKey("LowRes"))
                        {
                            foreach (KeyValuePair<string, string> LowResEntry in LowRes)
                            {
                                EpisodeDictionary["LowRes"].Add(LowResEntry.Key, LowResEntry.Value);
                            }
                        }
                        else
                        {
                            EpisodeDictionary.Add("LowRes", LowRes);
                        }
                    }
                    if (MidRes.Count > 0)
                    {
                        if (EpisodeDictionary.ContainsKey("MidRes"))
                        {
                            foreach (KeyValuePair<string, string> MidResEntry in MidRes)
                            {
                                EpisodeDictionary["MidRes"].Add(MidResEntry.Key, MidResEntry.Value);
                            }
                        }
                        else
                        {
                            EpisodeDictionary.Add("MidRes", MidRes);
                        }
                    }
                    if (HiRes.Count > 0)
                    {
                        if (EpisodeDictionary.ContainsKey("HiRes"))
                        {
                            foreach (KeyValuePair<string, string> HiResEntry in HiRes)
                            {
                                EpisodeDictionary["HiRes"].Add(HiResEntry.Key, HiResEntry.Value);
                            }
                        }
                        else
                        {
                            EpisodeDictionary.Add("HiRes", HiRes);
                        }
                    }
                }
            }

            return EpisodeDictionary;
        }

        private string extractShowID(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            string htmlPage;
            using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                htmlPage = reader.ReadToEnd();
            }

            string showID = string.Empty;
            if (!String.IsNullOrEmpty(htmlPage))
            {
                // <link rel="canonical" href="https://horriblesubs.info/shows/active-raid/" />
                string pattern = "<script type=\"text/javascript\">var hs_showid = (?<showID>.*?);</script>";
                //string pattern = "<link rel=\"canonical\" href=\"(?<showID>.*?)\" />";
                Match m = Regex.Match(htmlPage, pattern);

                showID = m.Groups["showID"].Value;
            }
            return showID;
        }
    }
}
