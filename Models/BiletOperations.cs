using TrenRezervasyonSistemi.Models.Entities;

namespace TrenRezervasyonSistemi.Models
{
    public class BiletOperations : IBiletOperations
    {
        private List<float> vagonDolulukOranlari = new List<float>();
        private SunucuCevap sunucuCevap = new SunucuCevap();
        private List<int> vagonBosKoltuk = new List<int>();
        private int toplamBosKoltukSayisi = 0;
        public SunucuCevap Yerlestir(Girdi girdi)
        {
            DolulukOraniHesapla(girdi.tren.Vagonlar);
            BosYerHesapla(girdi.tren);
            if (!girdi.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                for (int i = 0; i < vagonBosKoltuk.Count; i++)
                {
                    if (vagonBosKoltuk[i] > 0 && vagonBosKoltuk[i] - girdi.RezervasyonYapilacakKisiSayisi >= 0)
                    {
                        YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti();
                        yerlesimAyrinti.VagonAdi = girdi.tren.Vagonlar[i].Ad;
                        yerlesimAyrinti.KisiSayisi = girdi.RezervasyonYapilacakKisiSayisi;
                        sunucuCevap.YerlesimAyrinti.Add(yerlesimAyrinti);
                        sunucuCevap.RezervasyonYapılabilir = true;
                        return sunucuCevap;
                    }
                }
            }

            else
            {
                if (toplamBosKoltukSayisi > girdi.RezervasyonYapilacakKisiSayisi)
                {
                    for (int i = 0; i < vagonBosKoltuk.Count; i++)
                    {
                        if (vagonBosKoltuk[i] > 0 && girdi.RezervasyonYapilacakKisiSayisi > 0)
                        {

                            if (girdi.RezervasyonYapilacakKisiSayisi - vagonBosKoltuk[i] >= 0)
                            {
                                YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti();
                                yerlesimAyrinti.VagonAdi = girdi.tren.Vagonlar[i].Ad;
                                yerlesimAyrinti.KisiSayisi = vagonBosKoltuk[i];
                                sunucuCevap.YerlesimAyrinti.Add(yerlesimAyrinti);
                                girdi.RezervasyonYapilacakKisiSayisi -= vagonBosKoltuk[i];

                            }

                            else
                            {
                                YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti();
                                yerlesimAyrinti.VagonAdi = girdi.tren.Vagonlar[i].Ad;
                                yerlesimAyrinti.KisiSayisi = girdi.RezervasyonYapilacakKisiSayisi;
                                sunucuCevap.YerlesimAyrinti.Add(yerlesimAyrinti);
                                girdi.RezervasyonYapilacakKisiSayisi = 0;
                            }
                        }

                    }

                    sunucuCevap.RezervasyonYapılabilir = true;
                    return sunucuCevap;
                }
            }

            sunucuCevap.RezervasyonYapılabilir = false;
            return sunucuCevap;

        }
        private void DolulukOraniHesapla(Vagon[] vagonlar)
        {
            foreach (Vagon vagon in vagonlar)
            {
                float dolulukOrani = (float)vagon.DoluKoltukAdet / (float)vagon.Kapasite;
                vagonDolulukOranlari.Add(dolulukOrani);
            }

        }
        private void BosYerHesapla(Tren tren)
        {
            int bosKoltukSayisi;
            for (int i = 0; i < vagonDolulukOranlari.Count; i++)
            {
                if (vagonDolulukOranlari[i] <= 0.7)
                    bosKoltukSayisi = (int)(tren.Vagonlar[i].Kapasite * 0.7 - tren.Vagonlar[i].Kapasite * vagonDolulukOranlari[i]);

                else
                    bosKoltukSayisi = 0;

                toplamBosKoltukSayisi += bosKoltukSayisi;
                vagonBosKoltuk.Add(bosKoltukSayisi);
            }
        }
    }
}
