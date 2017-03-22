using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class CreateCourseViewModel
    {
        public ActivityCreateViewModel ActivityModel { get; set; }
        public ModuleCreateViewModel ModuleModel { get; set; }
        public Course Course { get; set; }
    }
}