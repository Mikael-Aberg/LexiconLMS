using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class DocumentCreateViewModel
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Ladda upp fil")]
        [Required(ErrorMessage = "Välj fil att ladda upp.")]
        public string file { get; set; }

        [DisplayName("Filnamn")]
        public string Name { get; set; }

        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Delad")]
        public bool Shared { get; set; }

        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
        public string UserId { get; set; }

    }
}