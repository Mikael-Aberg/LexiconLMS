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
        public ICollection<ScheduleLink> Morning { get; set; }
        public ICollection<ScheduleLink> Afternoon { get; set; }
    }
}