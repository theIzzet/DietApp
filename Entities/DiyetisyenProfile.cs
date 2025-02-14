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
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public DietUser User { get; set; } = null!;

        public string? About { get; set; }
        public string? ProfilePicturePath { get; set; }

        public ICollection<DietType> DietTypes { get; set; } = new List<DietType>();

        public ICollection<Comment > Comments { get; set; }=new List<Comment>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();

        public ICollection<PersonalInfo> Hastalar { get; set; } = new List<PersonalInfo>();
    }
}
