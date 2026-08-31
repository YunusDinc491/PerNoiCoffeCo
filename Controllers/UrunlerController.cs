using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;
using PerNoiWebsite.Context;

[Authorize]
public class UrunlerController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public UrunlerController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Urun.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UrunAdi,Icerik,Kalori")] Urun urun, IFormFile? FotoDosya)
    {
        if (FotoDosya != null && FotoDosya.Length > 0)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "urunler");
            Directory.CreateDirectory(klasor);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(FotoDosya.FileName);
            var dosyaYolu = Path.Combine(klasor, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await FotoDosya.CopyToAsync(stream);
            }

            urun.Foto = "/images/urunler/" + dosyaAdi;
            ModelState.Remove("Foto");
        }

        if (ModelState.IsValid)
        {
            _context.Add(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(urun);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var urun = await _context.Urun.FindAsync(id);
        if (urun == null) return NotFound();
        return View(urun);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,UrunAdi,Icerik,Kalori")] Urun urun, IFormFile? FotoDosya)
    {
        if (id != urun.Id) return NotFound();

        var mevcutFoto = await _context.Urun.AsNoTracking()
            .Where(u => u.Id == urun.Id)
            .Select(u => u.Foto)
            .FirstOrDefaultAsync();
        urun.Foto = mevcutFoto;

        if (FotoDosya != null && FotoDosya.Length > 0)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "urunler");
            Directory.CreateDirectory(klasor);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(FotoDosya.FileName);
            var dosyaYolu = Path.Combine(klasor, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await FotoDosya.CopyToAsync(stream);
            }

            urun.Foto = "/images/urunler/" + dosyaAdi;
        }

        ModelState.Remove("Foto");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(urun);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrunExists(urun.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(urun);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var urun = await _context.Urun.FirstOrDefaultAsync(m => m.Id == id);
        if (urun == null) return NotFound();
        return View(urun);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var urun = await _context.Urun.FindAsync(id);
        if (urun != null) _context.Urun.Remove(urun);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool UrunExists(int id)
    {
        return _context.Urun.Any(e => e.Id == id);
    }
}