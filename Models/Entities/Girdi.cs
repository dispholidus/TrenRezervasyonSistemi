namespace TrenRezervasyonSistemi.Models.Entities
{
    public class Girdi
    {
        public Tren tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }

    }
}
