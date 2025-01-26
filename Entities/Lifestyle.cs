using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;

namespace DietApp.Entities
{
    public class Lifestyle
    {
        [Key]
        public int Id { get; set; } 

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        public string? StressLevel { get; set; } 
         
        public string? NumberOfSmokingPackage { get; set; } 
        public string? SmokingUtilezeYear { get; set; } 
        public string? AlcoholConsumption { get; set; } 
        public string? CaffeineIntake { get; set; } 
        public string? MotivationLevel { get; set; } 
        public string? SocialSupport { get; set; }

        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }
    }
}
