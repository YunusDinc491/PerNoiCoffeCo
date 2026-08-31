using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;
using PerNoiWebsite.Context;
[Authorize]

public class HikayemizController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public HikayemizController(AppDbContext context, IWebHostEnvironment env )
    {
        _context = context;
        _env = env;
    }

    // GET: HIKAYEMIZS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Hikayemiz.ToListAsync());
    }

    // GET: HIKAYEMIZS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hikayemiz = await _context.Hikayemiz
            .FirstOrDefaultAsync(m => m.Id == id);
        if (hikayemiz == null)
        {
            return NotFound();
        }

        return View(hikayemiz);
    }

    // GET: HIKAYEMIZS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HIKAYEMIZS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,UstBaslik,AltMetin,OzluSoz")] Hikayemiz hikayemiz, IFormFile? FotoDosya)
    {
        if (FotoDosya != null && FotoDosya.Length > 0)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "hikayemiz");
            Directory.CreateDirectory(klasor);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(FotoDosya.FileName);
            var dosyaYolu = Path.Combine(klasor, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await FotoDosya.CopyToAsync(stream);
            }

            hikayemiz.Photo = "/images/hikayemiz/" + dosyaAdi;
            ModelState.Remove("Photo");
        }

        if (ModelState.IsValid)
        {
            _context.Add(hikayemiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(hikayemiz);
    }

    // GET: HIKAYEMIZS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hikayemiz = await _context.Hikayemiz.FindAsync(id);
        if (hikayemiz == null)
        {
            return NotFound();
        }
        return View(hikayemiz);
    }

    // POST: HIKAYEMIZS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,UstBaslik,AltMetin,OzluSoz")] Hikayemiz hikayemiz, IFormFile? FotoDosya)
    {
        if (id != hikayemiz.Id)
        {
            return NotFound();
        }

        var mevcutFoto = await _context.Hikayemiz.AsNoTracking()
            .Where(h => h.Id == hikayemiz.Id)
            .Select(h => h.Photo)
            .FirstOrDefaultAsync();
        hikayemiz.Photo = mevcutFoto;

        if (FotoDosya != null && FotoDosya.Length > 0)
        {
            var klasor = Path.Combine(_env.WebRootPath, "images", "hikayemiz");
            Directory.CreateDirectory(klasor);

            var dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(FotoDosya.FileName);
            var dosyaYolu = Path.Combine(klasor, dosyaAdi);

            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                await FotoDosya.CopyToAsync(stream);
            }

            hikayemiz.Photo = "/images/hikayemiz/" + dosyaAdi;
        }

        ModelState.Remove("Photo");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(hikayemiz);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HikayemizExists(hikayemiz.Id))
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
        return View(hikayemiz);
    }

    // GET: HIKAYEMIZS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var hikayemiz = await _context.Hikayemiz
            .FirstOrDefaultAsync(m => m.Id == id);
        if (hikayemiz == null)
        {
            return NotFound();
        }

        return View(hikayemiz);
    }

    // POST: HIKAYEMIZS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var hikayemiz = await _context.Hikayemiz.FindAsync(id);
        if (hikayemiz != null)
        {
            _context.Hikayemiz.Remove(hikayemiz);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HikayemizExists(int? id)
    {
        return _context.Hikayemiz.Any(e => e.Id == id);
    }
}
