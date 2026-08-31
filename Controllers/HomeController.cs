
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Context;
using PerNoiWebsite.Models;
using System.Diagnostics;


namespace PerNoiWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var menuler = _context.Menu
                .Include(p => p.CaylarVeDigerleri)
    .ToList();

            var hikaye = _context.Hikayemiz.FirstOrDefault();
            ViewBag.Hikaye = hikaye;

            var galeriler = _context.Galeri.ToList();
            ViewBag.Galeriler = galeriler;

            var urunler = _context.Urun.ToList();
            ViewBag.Urunler = urunler;

            var iletisim = _context.Iletisim.FirstOrDefault();
            ViewBag.Iletisim = iletisim;

            return View(menuler);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RezervasyonKaydet(Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _context.Rezervasyon.Add(rezervasyon);
                _context.SaveChanges();
                TempData["RezervasyonBasarili"] = "Rezervasyonunuz alındı, teşekkürler!";

            }
            return RedirectToAction("Index", "Home", null, "rezervasyon");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
