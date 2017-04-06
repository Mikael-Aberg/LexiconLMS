using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class FeedbackViewModel
    {
        public string StudentId { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
    }
}