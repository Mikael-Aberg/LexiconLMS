using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Uppladdnings tillfälle")]
        public DateTime UploadTime { get; set; }

        [DisplayName("Delad")]
        public bool Shared { get; set; }

        [DisplayName("Sökväg")]
        public string FilePath { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}