using System;

namespace DietApp.Entities
{
    public class KisiselBilgiler
    {
        public int Id { get; set; } // Birincil anahtar
        public string KullaniciId { get; set; } // Kullanıcıya bağlanacak
        public string AdSoyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public double Boy { get; set; }
        public double Kilo { get; set; }
        public string IletisimBilgileri { get; set; }
        public string Meslek { get; set; }
        public string MedeniDurum { get; set; }
        public int CocukSayisi { get; set; }
    }
}
