using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Meddelande")]
        public string Message { get; set; }
        [DisplayName("Skrivet av")]
        public string PostedBy { get; set; }
        [DisplayName("Skickat")]
        public DateTime PostedTime { get; private set; }

        [ForeignKey("PostedBy")]
        public virtual ApplicationUser User { get; set; }
        public virtual Feedback Feedback { get; set; }
    }
}