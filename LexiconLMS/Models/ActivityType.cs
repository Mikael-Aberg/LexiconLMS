using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        [DisplayName("Namn")]
        public string Name { get; set; }
        public bool IsAssignment { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}