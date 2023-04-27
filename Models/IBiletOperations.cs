using TrenRezervasyonSistemi.Models.Entities;

namespace TrenRezervasyonSistemi.Models
{
    public interface IBiletOperations
    {
        public SunucuCevap Yerlestir(Girdi girdi);
    }
}
