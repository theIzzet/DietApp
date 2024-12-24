namespace DietApp.Entities
{
    public class BeslenmeAliskanliklari
    {
        public int Id { get; set; } // Birincil anahtar
        public string KullaniciId { get; set; } // Kullanıcıya bağlanacak
        public int GunlukOgunSayisi { get; set; }
        public string OgunZamanlari { get; set; }
        public string TuketilenBesinler { get; set; }
        public string AtistirmaAliskanliklari { get; set; }
        public string SiviAlimi { get; set; }
        public string BesinIntoleranslari { get; set; }
        public string YemekPismeYontemi { get; set; }
        public string YemekYemeSuresi { get; set; }
        public string DisaridaYemekYemeAliskanligi { get; set; }
        public string TatliTuketimi { get; set; }
    }
}
