using DietApp.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietApp.Entities
{
    public class DiyetisyenProfile
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public DietUser User { get; set; }

        public string? About { get; set; }
        public string? ProfilePicturePath { get; set; }

        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();

        public ICollection<PersonalInfo> Hastalar { get; set; } = new List<PersonalInfo>();
    }
}
