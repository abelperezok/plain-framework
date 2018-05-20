using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class CustomDateTime
    {
        public CustomDateTime()
        {
            BeginYear = 1994;
            EndYear = DateTime.Now.Year + 5;
        }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int BeginYear { get; set; }

        public int EndYear { get; set; }

        public DateTime GetDateTime(DateTime defaultTime)
        {
            if (Day > 0 && Month > 0 && Year > 0)
            {
                return new DateTime(Year, Month, Day);
            }
            return defaultTime;
        }

        public DateTime GetPartDateTime(DateTime defaultTime)
        {
            if (Year > 0)
            {
                int _day = Day > 0 ? Day : 1;
                int _month = Month > 0 ? Month : 1;
                return new DateTime(Year, _month, _day);
            }
            return defaultTime;
        }

        public void SetDateTime(DateTime dateTime)
        {
            Day = dateTime.Day;
            Month = dateTime.Month;
            Year = dateTime.Year;
        }
    }
}