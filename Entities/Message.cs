using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Data;

namespace DietApp.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public string? SenderId { get; set; } 
        public string? ReceiverId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public DietUser Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public DietUser Receiver { get; set; }
    }
}
