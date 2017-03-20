using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Module Moduel { get; set; }
        public virtual ActivityType Type { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}