using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DietApp.Data;

namespace DietApp.Entities
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        public string? DiseaseName { get; set; } 

        public int? DiseaseYear { get; set; }

        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }

    }
}
