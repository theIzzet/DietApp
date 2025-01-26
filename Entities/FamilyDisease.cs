using DietApp.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietApp.Entities
{
    public class FamilyDisease
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        public string? DiseaseName { get; set; }

        
        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }
    }
}
