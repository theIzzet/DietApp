using Microsoft.EntityFrameworkCore;
using DietApp.Entities;

namespace DietApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<KisiselBilgiler> KisiselBilgiler { get; set; }
        public DbSet<TibbiGecmis> TibbiGecmis { get; set; }
        public DbSet<BeslenmeAliskanliklari> BeslenmeAliskanliklari { get; set; }
        public DbSet<FizikselAktiviteDurumu> FizikselAktiviteDurumu { get; set; }
        public DbSet<YasamTarzi> YasamTarzi { get; set; }
        public DbSet<Hedefler> Hedefler { get; set; }
    }
}
