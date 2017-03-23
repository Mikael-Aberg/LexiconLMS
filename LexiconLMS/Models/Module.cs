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
        public int? Id { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett modulnamn.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Modulnamnet får max vara 200 tecken långt.")]
        [DisplayName("Modulnamn")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Beskrivningen får max vara 500 tecken långt.")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Startdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett startdatum.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [DisplayName("Slutdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett slutdatum.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}