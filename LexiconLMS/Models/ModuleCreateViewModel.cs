using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ModuleCreateViewModel
    {
        public Module Module { get; set; }
        public ICollection<Module> Modules { get; set; }
        public int? CourseId { get; set; }
    }
}