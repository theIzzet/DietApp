using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietApp.Models
{
    public class PatientInfoViewModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? SurName { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string? Gender { get; set; }
        [Required]
        public int? Height { get; set; }
        [Required]
        public double? Weight { get; set; }
        [Required]
        public string? ContactInformation { get; set; }

        [Required]
        public string? Occupation { get; set; } // meslek
        [Required]
        public string? MaritalStatus { get; set; } // medeni durum
        [Required]
        public int? NumberOfChildren { get; set; }

        [Required]
        public string? RegularPhysicalActivity { get; set; }
        [Required]
        public string? DailyInactivity { get; set; } // hareketsizlik
        [Required]
        public string? SleepPattern { get; set; }  //uyku düzeni

        


        public string? StressLevel { get; set; }
        
        
        
        public string? NumberOfSmokingPackage { get; set; }
        public string? SmokingUtilezeYear { get; set; }
        public string? AlcoholConsumption { get; set; }
        public string? CaffeineIntake { get; set; }
        public string? MotivationLevel { get; set; }
        public string? SocialSupport { get; set; }




        public string? WeightGoal { get; set; } // KiloHedefi 
        public string? HealthIssuesManagement { get; set; } // SaglikSorunlariYonetimi 
        public string? SportsPerformanceGoals { get; set; } // SporPerformansiHedefleri 
        public string? OtherGoals { get; set; }


        



        //[Required]
        //public int? DailyMealCount { get; set; } //öğün sayısı
        public string? MealTimes { get; set; } // zamanlar
        public string? ConsumedFoods { get; set; } // yemek tüketimi
        [Required]
        public string? SnackingHabits { get; set; }  //atıştırma alışkanlıkları
        [Required]
        public string? WaterConsumption { get; set; } // su tüketimi
        //[Required]
        //public string? FoodIntolerances { get; set; } // 
        public string? CookingMethod { get; set; } // YemekPismeYontemi
        public string? EatingDuration { get; set; } // YemekYemeSuresi
        public string? EatingOutHabits { get; set; } // dışarda yenme alışkanlığı
        public string? DessertConsumption { get; set; }  // tatlı tüketimi


        
        public string? DiseaseName { get; set; }

       
        public string? FamilyDiseaseName { get; set; }

        
        public string? AllergyName { get; set; }
        
        public string? MedicationName { get; set; }




        public List<string>? AllergyNames { get; set; } = new List<string>();

        public List<string>? DiseaseNameDs { get; set; } = new List<string>();

        public List<string>? FamilyDiseaseNames { get; set; } = new List<string>();


        public List<string>? MedicationNames { get; set; } = new List<string>();
    }
}
