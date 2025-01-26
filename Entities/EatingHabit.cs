using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;

namespace DietApp.Entities
{
    public class EatingHabit
    {
        [Key]
        public int Id { get; set; } 

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty; 
        
        public int? DailyMealCount { get; set; } //öðün sayýsý
        public string? MealTimes { get; set; } // zamanlar
        public string? ConsumedFoods { get; set; } // yemek tüketimi
        public string? SnackingHabits { get; set; }  //atýþtýrma alýþkanlýklarý
        public string? WaterConsumption { get; set; } // su tüketimi
        public string? FoodIntolerances { get; set; } // 
        public string? CookingMethod { get; set; } // YemekPismeYontemi
        public string? EatingDuration { get; set; } // YemekYemeSuresi
        public string? EatingOutHabits { get; set; } // dýþarda yenme alýþkanlýðý
        public string? DessertConsumption { get; set; }  // tatlý tüketimi

        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; } 
    }
}
