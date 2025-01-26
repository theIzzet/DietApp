using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;

namespace DietApp.Entities
{
    public class PersonalInfo
    {
        [Key]
        public int Id { get; set; } 

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public DateTime? DateOfBirth { get; set; } 
        public string? Gender { get; set; } 
        public int? Height { get; set; } 
        public double? Weight { get; set; } 
        public string? ContactInformation { get; set; } 
        public string? Occupation { get; set; } // meslek
        public string? MaritalStatus { get; set; } // medeni durum
        public int? NumberOfChildren { get; set; }

        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }
    }
}
