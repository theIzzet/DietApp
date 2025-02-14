using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietApp.Entities
{
    public class DietType
    {
        [Key]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? About { get; set; }

        public string? PicturePath { get; set; }

        

        public ICollection<DiyetisyenProfile> DiyetisyenProfiles { get; set; }=new List<DiyetisyenProfile>();
    }
}
