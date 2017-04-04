using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string PostedById { get; set; }
        public string PostedToId { get; set; }
        public int ActivityId { get; set; }

        public string Message { get; set; }
        public DateTime PostedDate { get; private set; }

        public bool IsPostedByTeacher { get; set; }

        [ForeignKey("PostedById")]
        public virtual ApplicationUser PostedBy { get; set; }
        [ForeignKey("PostedToId")]
        public virtual ApplicationUser PostedTo { get; set; }
        public virtual Activity Activity { get; set; }
    }
}