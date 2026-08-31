namespace PerNoiWebsite.Models
{
    public class Rezervasyon
    {
        public int Id { get; set; }

        public string AdSoyad { get; set; }
        public string MusteriTelefon { get; set; }
        public DateOnly Tarih { get; set; }
        public string Saat { get; set; }
        public string KisiSayisi { get; set; }
        public string Not { get; set; }
    }
}
