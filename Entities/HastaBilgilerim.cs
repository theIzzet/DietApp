using DietApp.Entities;

namespace DietApp.Entities
{
    public class HastaBilgilerim
    {
        public PersonalInfo? KisiselBilgiler { get; set; } 
        
        public EatingHabit? BeslenmeAliskanliklari { get; set; } 
        public PhysicalActivityStatus? FizikselAktiviteDurumu { get; set; }  
        public Lifestyle? YasamTarzi { get; set; } 
        public Goal? Hedefler { get; set; } 

        public Allergy? Allergy { get; set; } 

        public Disease? Disease { get; set; }  
        public FamilyDisease? FamilyDisease { get; set;} 
        public Medication? Medication { get; set; } 

        public PastMedical? TibbiGecmis { get; set; }
    }
}