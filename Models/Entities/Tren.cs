namespace TrenRezervasyonSistemi.Models.Entities
{
    public class Tren
    {
        public string Ad { get; set; } = string.Empty;
        public Vagon[] Vagonlar { get; set; }
    }
}