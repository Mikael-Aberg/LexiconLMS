using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ScheduleViewModel
    {
        public string Name { get; set; }
        public ICollection<SchedulePost> Schedule { get; set; }
    }
}