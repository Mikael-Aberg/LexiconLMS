using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpploadTime { get; set; }
        public bool Shared { get; set; }
        public string FilePath { get; set; }


        public virtual ApplicationUser User { get; set; }
    }
}