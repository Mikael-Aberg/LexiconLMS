using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ModuleListViewModel
    {
        public int Id { get; set; }

        [DisplayName("Kursnamn")]
        public string CourseName { get; set; }

        public ICollection<Module> Modules { get; set; }
    }
}