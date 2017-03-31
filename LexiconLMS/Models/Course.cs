using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Kursnamn")]
        [Required(ErrorMessage = "Du måste fylla i ett kursnamn.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Kursnamnet får max vara 200 tecken långt.")]
        public string Name { get; set; }

        [Display(Name = "Beskrivning")]    
        [StringLength(500, ErrorMessage = "Beskrivningen får max vara 500 tecken långt.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Beskrivning")]
        public string ShortDescription { get { return (Description != null) ? (Description.Length > 20) ? Description.Substring(0, 20) + "..." : Description : ""; } }

        [Display(Name = "Startdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett startdatum.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett slutdatum.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}