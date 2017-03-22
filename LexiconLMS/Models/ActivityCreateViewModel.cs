using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Models
{
    public class ActivityCreateViewModel
    {
        [DisplayName("Aktivitet")]
        public Activity Activity { get; set; }

        [DisplayName("Modul")]
        public SelectList ModuleList { get; set; }

        [DisplayName("Typ")]
        public SelectList Types { get; set; }

        public int? CourseId { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}