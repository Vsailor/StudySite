using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudySite.Services
{
    public class TimeService
    {
        public static int GetUnixTime(DateTime dateTime)
        {
            return (int)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds;
        }

        public static DateTime GetDateTime(int dateTimeUnix)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(dateTimeUnix);
        }
    }
}