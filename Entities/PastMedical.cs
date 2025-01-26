using DietApp.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietApp.Entities
{
    public class PastMedical
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        public string? AllergyName { get; set; }

        public string? DiseaseName { get; set; }

        

        public string? FamilyDiseaseName { get; set; }

        public string? MedicationName { get; set; }
        //[ForeignKey(nameof(UserId))]
        public DietUser? User { get; set; }
    }
}
