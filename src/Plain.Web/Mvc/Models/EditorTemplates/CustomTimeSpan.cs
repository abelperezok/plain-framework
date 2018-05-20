using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class CustomTimeSpan
    {
        public CustomTimeSpan()
        {}

        public CustomTimeSpan(TimeSpan timeSpan)
        {
            Hours = timeSpan.Hours;
            Minutes = timeSpan.Minutes;
            Seconds = timeSpan.Seconds;
        }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public TimeSpan GetTimeSpan()
        {
            return new TimeSpan(Hours, Minutes, Seconds);
        }
    }
}