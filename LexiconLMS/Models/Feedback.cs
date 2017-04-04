using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string ApplicationUserId { get; set; }
        public bool PassingGrade { get; set; }

        public virtual ApplicationUser Student { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual ICollection<FeedBackMessage> Messages { get; set; }
    }
}