using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PerNoiWebsite.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string kullaniciAdi, string sifre)
        {
            var dogruKullaniciAdi = _configuration["AdminCredentials:Username"];
            var dogruSifre = _configuration["AdminCredentials:Password"];

            if (kullaniciAdi == dogruKullaniciAdi && sifre == dogruSifre)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, kullaniciAdi)
                };
                var kimlik = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(kimlik));

                return RedirectToAction("Index", "Menus");
            }

            ViewBag.Hata = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
    

