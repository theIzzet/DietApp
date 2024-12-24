namespace DietApp.Entities
{
    public class Hedefler
    {
        public int Id { get; set; } // Birincil anahtar
        public string KullaniciId { get; set; } // Kullanıcıya bağlanacak
        public string KiloHedefi { get; set; }
        public string SaglikSorunlariYonetimi { get; set; }
        public string SporPerformansiHedefleri { get; set; }
        public string DigerHedefler { get; set; }
    }
}
