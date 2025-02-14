namespace DietApp.Models
{
    public class CommentViewModel
    {
        public int DietisyenProfileId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
