using DietApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Data
{
    public class IdentityContext : IdentityDbContext<DietUser, DietRole, string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
        public DbSet<PersonalInfo> PersonalInfos => Set<PersonalInfo>();
        public DbSet<PastMedical> PastMedicals => Set<PastMedical>();
        public DbSet<EatingHabit> EatingHabits => Set<EatingHabit>();
        public DbSet<PhysicalActivityStatus> PhysicalActivityStatus => Set<PhysicalActivityStatus>();
        public DbSet<Lifestyle> Lifestyles => Set<Lifestyle>();
        public DbSet<Goal> Goals => Set<Goal>();

    }
}
