namespace DietApp.Entities
{
    public class YasamTarzi
    {
        public int Id { get; set; } // Birincil anahtar
        public string KullaniciId { get; set; } // Kullanıcıya bağlanacak
        public string StresDuzeyi { get; set; }
        public string SigaraKullanimi { get; set; }
        public string AlkolTuketimi { get; set; }
        public string KafeinAlimi { get; set; }
        public string MotivasyonDurumu { get; set; }
        public string SosyalDestek { get; set; }
    }
}
