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
        [Required(ErrorMessage = "Du måste fylla i en beskrivning.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Beskrivningen får max vara 500 tecken långt.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett startdatum.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett slutdatum.")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}