using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Start tid")]
        public DateTime StartTime { get; set; }
        [DisplayName("Slut tid")]
        public DateTime EndTime { get; set; }

        public virtual Module Moduel { get; set; }
        [DisplayName("Typ")]
        public virtual ActivityType Type { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}