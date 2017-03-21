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

        [DisplayName("Namn")]
        [Required]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Start tid")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DisplayName("Slut tid")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
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