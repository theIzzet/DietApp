using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Entities
{
    public class PhysicalActivityStatus
    {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty; 
        public string? RegularPhysicalActivity { get; set; } 
        public string? DailyInactivity { get; set; } // hareketsizlik
        public string? SleepPattern { get; set; }  //uyku düzeni

        [ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }

    }
}
