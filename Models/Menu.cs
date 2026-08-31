namespace PerNoiWebsite.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Baslik { get; set; }

        public List<MenuCaylarveDigerleri> CaylarVeDigerleri { get; set; } = new();

    }
}
