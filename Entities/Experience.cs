using DietApp.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DietApp.Entities
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public DietUser User { get; set; }
    }
}
