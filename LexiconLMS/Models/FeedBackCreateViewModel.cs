using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class FeedBackCreateViewModel
    {
        public string StudentId { get; set; }
        public string PostedById { get; set; }
        public int FeedbackId { get; set; }
        [DisplayName("Meddelande")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}