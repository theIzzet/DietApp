using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace DietApp.Entities
{
    public class BodyMeasurement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }

        public DateTime MeasurementDate { get; set; }
        public double? Waist { get; set; }    // Bel
        public double? Hips { get; set; }     // Kalça
        public double? Chest { get; set; }    // Göğüs
        public double? UpperArm { get; set; } // Üst Kol
        public double? Leg { get; set; }      // Bacak
        public double? Neck { get; set; }     // Boyun
        public string? PhotoPath { get; set; }
    }
}
