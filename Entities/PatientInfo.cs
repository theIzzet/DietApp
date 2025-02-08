using DietApp.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietApp.Entities
{
    public class PatientInfo
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

        public string? RegularPhysicalActivity { get; set; }
        public string? DailyInactivity { get; set; } // hareketsizlik
        public string? SleepPattern { get; set; }  //uyku düzeni



        public string? StressLevel { get; set; }
        
        public string? NumberOfSmokingPackage { get; set; }
        public string? SmokingUtilezeYear { get; set; }
        public string? AlcoholConsumption { get; set; }
        public string? CaffeineIntake { get; set; }
        public string? MotivationLevel { get; set; }
        public string? SocialSupport { get; set; }



        //Goal

        public string? WeightGoal { get; set; } // KiloHedefi 
        public string? HealthIssuesManagement { get; set; } // SaglikSorunlariYonetimi 
        public string? SportsPerformanceGoals { get; set; } // SporPerformansiHedefleri 
        public string? OtherGoals { get; set; }





        //EatingHabits
        public int? DailyMealCount { get; set; } //öğün sayısı
        public string? MealTimes { get; set; } // zamanlar
        public string? ConsumedFoods { get; set; } // yemek tüketimi
        public string? SnackingHabits { get; set; }  //atıştırma alışkanlıkları
        public string? WaterConsumption { get; set; } // su tüketimi
        public string? FoodIntolerances { get; set; } // 
        public string? CookingMethod { get; set; } // YemekPismeYontemi
        public string? EatingDuration { get; set; } // YemekYemeSuresi
        public string? EatingOutHabits { get; set; } // dışarda yenme alışkanlığı
        public string? DessertConsumption { get; set; }  // tatlı tüketimi



       

        public DietUser? User { get; set; }
    }
}
