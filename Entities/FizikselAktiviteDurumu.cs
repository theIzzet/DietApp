namespace DietApp.Entities
{
    public class FizikselAktiviteDurumu
    {
        public int Id { get; set; } // Birincil anahtar
        public string KullaniciId { get; set; } // Kullanıcıya bağlanacak
        public string DuzenliFizikselAktivite { get; set; }
        public string GunlukHareketsizlik { get; set; }
        public string UykuDuzeni { get; set; }
    }
}
