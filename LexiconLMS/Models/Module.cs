using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Module
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Start datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayName("Slut datum")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}