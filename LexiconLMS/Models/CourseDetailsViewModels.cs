using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Kursnamn")]
        public string Name { get; set; }
        [Display(Name = "Startdatum")]
        public string StartDate { get; set; }
        [Display(Name = "Slutdatum")]
        public string EndDate { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Beskrivning")]
        public string ShortDescription { get { return (Description != null) ? (Description.Length > 17) ? Description.Substring(0, 17) + "..." : Description : ""; } }
        public string ToolTipText { get { return (Description != null) ? (Description.Length > 17) ? Description : "" : ""; } }
        public List<CourseDetailsModuleViewModel> Modules { get; set; }
    }

    public class CourseDetailsModuleViewModel
    {
        public int Id { get; set; }
        [DisplayName("Modulnamn")]
        public string Name { get; set; }
        [DisplayName("Startdatum")]
        public string StartDate { get; set; }
        [DisplayName("Slutdatum")]
        public string EndDate { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Beskrivning")]
        public string ShortDescription { get { return (Description != null) ? (Description.Length > 17) ? Description.Substring(0, 17) + "..." : Description : ""; } }
        public string ToolTipText { get { return (Description != null) ? (Description.Length > 17) ? Description : "" : ""; } }
        public List<CourseDetailsActivityViewModel> Activities { get; set; }

    }

    public class CourseDetailsActivityViewModel
    {
        public int Id { get; set; }
        [DisplayName("Aktivitetsnamn")]
        public string Name { get; set; }
        [DisplayName("Startdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime { get; set; }
        [DisplayName("Slutdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Beskrivning")]
        public string ShortDescription { get { return (Description != null) ? (Description.Length > 17) ? Description.Substring(0, 17) + "..." : Description : ""; } }
        public string ToolTipText { get { return (Description != null) ? (Description.Length > 17) ? Description : "" : ""; } }
        [DisplayName("Typ")]
        public string TypeName { get; set; }
        public bool IsAssignment { get; set; }
        public string DocumentString { get; set; }

    }
}
