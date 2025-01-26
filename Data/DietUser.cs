using System.ComponentModel.DataAnnotations;
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

        //public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
        //public ICollection<Disease> Diseases { get; set; } = new List<Disease>();
        //public ICollection<FamilyDisease> FamilyDiseases { get; set; } = new List<FamilyDisease>();
        //public ICollection<Medication> Medications { get; set; } = new List<Medication>();

    }
}
