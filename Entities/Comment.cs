using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;

namespace DietApp.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime PublishedOn { get; set; }

        [Range(1, 5)] 
        public int Rating { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public DietUser User { get; set; } = null!;


        [ForeignKey(nameof(DP))]
        public int DPId { get; set; }

        public DiyetisyenProfile DP { get; set;} = null!;

    }
}
