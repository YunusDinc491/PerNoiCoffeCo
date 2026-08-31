using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;

namespace PerNoiWebsite.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Anasayfa> Anasayfa { get; set; }
        public DbSet<Galeri> Galeri { get; set; }
        public DbSet<Hikayemiz> Hikayemiz { get; set; }
        public DbSet<MenuCaylarveDigerleri> MenuCaylarveDigerleri { get; set; }
        public DbSet<Menu> Menu { get; set; }
      
       
        public DbSet<Rezervasyon> Rezervasyon { get; set; }
        public DbSet<SayfaSonu> SayfaSonu { get; set; }
        public DbSet<Yorumlar> Yorumlar { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Urun> Urun { get; set; }

    }
}
