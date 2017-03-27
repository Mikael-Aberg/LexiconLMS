using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class SchedulePost
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string Module { get; set; }
        public string Morning { get; set; }
        public string Afternoon { get; set; }
    }
}