using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Feedback
    {
        public Feedback()
        {
            PostedDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int ActivityId { get; set; }

        public string Message { get; set; }
        public DateTime PostedDate { get; private set; }

        public bool IsPostedByTeacher { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Activity Activity { get; set; }
    }
}