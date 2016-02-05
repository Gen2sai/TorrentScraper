using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TorrentScraper.Class_Library
{
    class HorribleSubs
    {
        public void fetchAnimeList()
        {
            using (var driver = new ChromeDriver(@"c:\users\yt.chong\documents\visual studio 2013\Projects\TorrentScraper\TorrentScraper"))
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(120));
                driver.Navigate().GoToUrl("http://horriblesubs.info/shows/");

                Thread.Sleep(10000);

                //WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(10));
                //wait.Until(x => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

                string htmlPage = driver.PageSource;
                Dictionary<string, string> animeDictionary = new Dictionary<string, string>();

                if (!String.IsNullOrEmpty(htmlPage))
                {
                    string pattern = "<div class=\"ind-show linkful\"><a href=\"(?<url>.*?)\"[\\s\\S].*?>(?<title>.*?)</a></div>";
                    MatchCollection m = Regex.Matches(htmlPage, pattern);

                    foreach(Match match in m)
                    {
                        animeDictionary.Add(match.Groups["title"].Value, match.Groups["url"].Value);
                    }
                }
            }
        }
    }
}
