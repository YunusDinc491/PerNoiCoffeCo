using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;
using PerNoiWebsite.Context;

[Authorize]
public class GalerisController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public GalerisController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

   
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Galeri.ToListAsync());
    }

   
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var galeri = await _context.Galeri
            .FirstOrDefaultAsync(m => m.Id == id);
        if (galeri == null)
        {
            return NotFound();
        }

        return View(galeri);
    }

    // GET: GALERIS/Create
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Baslik,FotoAciklama")] Galeri galeri, IFormFile? FotoDosya)
    {
        if (FotoDosya != null && FotoDosya.Length > 0)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "galeri");
            Directory.CreateDirectory(klasor);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(FotoDosya.FileName);
            var dosyaYolu = Path.Combine(klasor, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await FotoDosya.CopyToAsync(stream);
            }

            galeri.Foto = "/images/galeri/" + dosyaAdi;
            ModelState.Remove("Foto");
        }

        if (ModelState.IsValid)
        {
            _context.Add(galeri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(galeri);
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var galeri = await _context.Galeri.FindAsync(id);
        if (galeri == null)
        {
            return NotFound();
        }
        return View(galeri);
    }

    // POST: GALERIS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Baslik,FotoAciklama")] Galeri galeri, IFormFile? FotoDosya)
    {
        if (id != galeri.Id)
        {
            return NotFound();
        }

        var mevcutFoto = await _context.Galeri.AsNoTracking()
            .Where(g => g.Id == galeri.Id)
            .Select(g => g.Foto)
            .FirstOrDefaultAsync();
        galeri.Foto = mevcutFoto;

        if (FotoDosya != null && FotoDosya.Length > 0)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "galeri");
            Directory.CreateDirectory(klasor);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(FotoDosya.FileName);
            var dosyaYolu = Path.Combine(klasor, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await FotoDosya.CopyToAsync(stream);
            }

            galeri.Foto = "/images/galeri/" + dosyaAdi;
        }

        ModelState.Remove("Foto");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(galeri);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GaleriExists(galeri.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(galeri);
    }

    // GET: GALERIS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var galeri = await _context.Galeri
            .FirstOrDefaultAsync(m => m.Id == id);
        if (galeri == null)
        {
            return NotFound();
        }

        return View(galeri);
    }

    // POST: GALERIS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var galeri = await _context.Galeri.FindAsync(id);
        if (galeri != null)
        {
            _context.Galeri.Remove(galeri);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GaleriExists(int? id)
    {
        return _context.Galeri.Any(e => e.Id == id);
    }
}
