namespace PerNoiWebsite.Models
{
    public class Yorumlar
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Yorum { get; set; }

        public string KisiAdi { get; set; }
        public string Lakap { get; set; }
        public string Photo { get; set; }
    }
}
