using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int Id { get; set; }

        [DisplayName("Filnamn")]
        public string Name { get; set; }

        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Uppladdningstillfälle")]
        public DateTime UploadTime { get; set; }

        [DisplayName("Delad")]
        public bool Shared { get; set; }

        [DisplayName("Sökväg")]
        public string FilePath { get; set; }

        [DisplayName("Storlek")]
        public int ContentLength { get; set; }

        [DisplayName("Filtyp")]
        public string ContentType { get; set; }

        public string UserId { get; set; }
        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Course Course { get; set; }
        public virtual Module Module { get; set; }
        public virtual Activity Activity { get; set; }

    }
}