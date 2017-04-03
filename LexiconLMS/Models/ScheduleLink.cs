using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ScheduleLink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAssignment { get; set; }
    }
}