using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plain.Web.Mvc.Models
{
    public class Result
    {
        public Result()
        {
            Success = true;
            Messages = new List<Message>();
        }

        public bool Success { get; set; }

        public List<Message> Messages { get; set; }
    }
}
