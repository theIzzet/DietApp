using DietApp.Entities;

namespace DietApp.Entities
{
    public class HastaBilgilerim
    {
        public KisiselBilgiler KisiselBilgiler { get; set; }
        public TibbiGecmis TibbiGecmis { get; set; }
        public BeslenmeAliskanliklari BeslenmeAliskanliklari { get; set; }
        public FizikselAktiviteDurumu FizikselAktiviteDurumu { get; set; }
        public YasamTarzi YasamTarzi { get; set; }
        public Hedefler Hedefler { get; set; }
    }
}
