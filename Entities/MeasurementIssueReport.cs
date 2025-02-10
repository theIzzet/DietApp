using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietApp.Entities
{
    public class MeasurementIssueReport
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; set; }

        public DateTime ReportDate { get; set; }
        public string MeasurementType { get; set; }  // "Weight" veya "Body"
        public string Reason { get; set; }
        public bool ApprovedByDietitian { get; set; }
    }
}
