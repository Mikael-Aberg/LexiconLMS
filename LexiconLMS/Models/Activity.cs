using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett aktivitetsnamn.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Aktivitetsnamnet får max vara 200 tecken långt.")]
        [DisplayName("Aktivitetsnamn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Du måste fylla i en beskrivning.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Beskrivningen får max vara 500 tecken långt.")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Startdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett startdatum.")]
        public DateTime StartTime { get; set; }

        [DisplayName("Slutdatum")]
        [Required(ErrorMessage = "Du måste fylla i ett slutdatum.")]
        public DateTime EndTime { get; set; }

        public int ModuelId { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("ModuelId")]
        [DisplayName("Modul")]
        public virtual Module Module { get; set; }

        [ForeignKey("TypeId")]
        [DisplayName("Typ")]
        public virtual ActivityType Type { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}