using DietApp.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietApp.Entities
{
    public class WeightMeasurement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }

        public DateTime MeasurementDate { get; set; }
        public double Weight { get; set; }
        public string? PhotoPath { get; set; }
    }
}
