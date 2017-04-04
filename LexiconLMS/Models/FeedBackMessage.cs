using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class FeedBackMessage
    {
        public FeedBackMessage() { PostedTime = DateTime.Now; }

        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public string Message { get; set; }
        public string PostedBy { get; set; }
        public DateTime PostedTime { get; private set; }

        [ForeignKey("PostedBy")]
        public ApplicationUser User { get; set; }
        public Feedback Feedback { get; set; }
    }
}