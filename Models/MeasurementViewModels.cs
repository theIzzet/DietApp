using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using DietApp.Entities;

namespace DietApp.Models
{
    public class WeightMeasurementViewModel
    {
        [Required(ErrorMessage = "Kilo değeri gereklidir.")]
        public double Weight { get; set; }
        public IFormFile? Photo { get; set; }
    }

    public class BodyMeasurementViewModel
    {
        [Display(Name = "Bel (cm)")]
        public double? Waist { get; set; }
        [Display(Name = "Kalça (cm)")]
        public double? Hips { get; set; }
        [Display(Name = "Göğüs (cm)")]
        public double? Chest { get; set; }
        [Display(Name = "Üst Kol (cm)")]
        public double? UpperArm { get; set; }
        [Display(Name = "Bacak (cm)")]
        public double? Leg { get; set; }
        [Display(Name = "Boyun (cm)")]
        public double? Neck { get; set; }
        public IFormFile? Photo { get; set; }
    }

    public class MeasurementHistoryViewModel
    {
        public List<WeightMeasurement> WeightMeasurements { get; set; } = new List<WeightMeasurement>();
        public List<BodyMeasurement> BodyMeasurements { get; set; } = new List<BodyMeasurement>();
    }

    public class MeasurementIssueViewModel
    {
        public string MeasurementType { get; set; }
        [Required(ErrorMessage = "Açıklama gereklidir.")]
        [Display(Name = "Ölçüm Yapılamama Nedeni")]
        public string Reason { get; set; }
    }
}
