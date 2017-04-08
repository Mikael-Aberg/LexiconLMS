using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace LexiconLMS.Models
{
    public class AssignmentsListViewModel
    {
        public int ActivityId { get; set; }
        [DisplayName("Kurs")]
        public string CourseName { get; set; }
        [DisplayName("Modul")]
        public string ModuleName { get; set; }
        [DisplayName("Uppgift")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        public int DocumentCount { get; set; }
        public bool ShowFeedback { get; set; }
        public int? CourseId { get; set; }
        [DisplayName("Sista inlämningstillfälle")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime Deadline { get; set; }

        public string ShortDescription { get { return (Description != null) ? (Description.Length > 20) ? Description.Substring(0, 20) + "..." : Description : ""; } }
        public string ToolTipText { get { return (Description != null) ? (Description.Length > 20) ? Description : "" : ""; } }
    }
}