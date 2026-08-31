namespace PerNoiWebsite.Models
{
    public class MenuCaylarveDigerleri
    {
        public int Id { get; set; }
        public string IcecekAdi { get; set; }
        public string Aciklama { get; set; }
        public int Fiyat { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
