using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TorrentScraper.Class_Library
{
    class TimeHelper
    {
        public static int EpochConverter(DateTime DateTimeToConvert)
        {
            //epoch or unix TimeStamp Conversion
            int UnixTime = (Int32)(DateTimeToConvert.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return UnixTime;
        }
    }
}
