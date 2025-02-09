using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;

namespace DietApp.Entities
{
    public class DietList
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty; // Diyet içeriği

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int PersonalInfoId { get; set; } // Hastaya bağlanacak

        [ForeignKey(nameof(PersonalInfoId))]
        public PersonalInfo Patient { get; set; }
    }
}
