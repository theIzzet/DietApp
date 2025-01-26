using Microsoft.EntityFrameworkCore;
using DietApp.Entities;

namespace DietApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        //public DbSet<Allergy> Allergies  =>Set<Allergy>();
        //public DbSet<Disease> Diseases  =>Set<Disease>();
        //public DbSet<FamilyDisease> FamilyDiseases  =>Set<FamilyDisease>();
        //public DbSet<Medication> Medications  =>Set<Medication>();   

        //public DbSet<PersonalInfo> PersonalInfos => Set<PersonalInfo>();
        //public DbSet<PastMedical> PastMedicals => Set<PastMedical>();
        //public DbSet<EatingHabit> EatingHabits => Set<EatingHabit>();
        //public DbSet<PhysicalActivityStatus> PhysicalActivityStatus => Set<PhysicalActivityStatus>();
        //public DbSet<Lifestyle> Lifestyles => Set<Lifestyle>();
        //public DbSet<Goal> Goals => Set<Goal>();


  


    }
}
