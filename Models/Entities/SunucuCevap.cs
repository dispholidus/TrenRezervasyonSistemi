namespace TrenRezervasyonSistemi.Models.Entities
{
    public class SunucuCevap
    {
        public bool RezervasyonYapılabilir { get; set; } = false;
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; } = new List<YerlesimAyrinti>();
    }
}
