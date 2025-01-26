using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DietApp.Data;

namespace DietApp.Entities
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        public string? WeightGoal { get; set; } // KiloHedefi 
        public string? HealthIssuesManagement { get; set; } // SaglikSorunlariYonetimi 
        public string? SportsPerformanceGoals { get; set; } // SporPerformansiHedefleri 
        public string? OtherGoals { get; set; }

        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }
    }
}
