using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DietApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace DietApp.Data
{
    public class DietUser:IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string SurName {  get; set; } = string.Empty;
        
        public string? GraduationSertificatePath {  get; set; } 
        public string? TranskriptPath { get; set;} 

       
        public EatingHabit? EatingHabit { get; set; }
        public PhysicalActivityStatus? PhysicalActivityStatus { get; set; }

        public Goal? Goal {  get; set; }
        public PersonalInfo? PersonalInfo { get; set; }

        public Lifestyle? Lifestyle { get; set; } 

        public PastMedical? PastMedical { get; set; }


        [InverseProperty(nameof(Message.Sender))]
        public ICollection <Message> SentMessages { get; set; }= new List<Message>();

        [InverseProperty(nameof(Message.Receiver))]
        public ICollection <Message> ReceivedMessages { get; set; } = new List<Message>();

    }
}
