using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class SchedulePost
    {
        [DisplayName("Datum")]
        public string Date { get; set; }
        [DisplayName("Dag")]
        public string Day { get; set; }
        [DisplayName("Modul")]
        public string Module { get; set; }
        [DisplayName("Förmiddag")]
        public ICollection<ScheduleLink> Morning { get; set; }
        [DisplayName("Eftermiddag")]
        public ICollection<ScheduleLink> Afternoon { get; set; }
    }
}