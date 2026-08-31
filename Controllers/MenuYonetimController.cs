using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Context;
using PerNoiWebsite.Models;

[Authorize]
public class MenuYonetimController : Controller
{
    private readonly AppDbContext _context;

    public MenuYonetimController(AppDbContext context)
    {
        _context = context;
    }

    // GET: MENUCAYLARVEDIGERLERIS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.MenuCaylarveDigerleri.ToListAsync());
    }

    // GET: MENUCAYLARVEDIGERLERIS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menucaylarvedigerleri = await _context.MenuCaylarveDigerleri
            .FirstOrDefaultAsync(m => m.Id == id);
        if (menucaylarvedigerleri == null)
        {
            return NotFound();
        }

        return View(menucaylarvedigerleri);
    }

    public IActionResult Create()
    {
        ViewBag.Menuler = new SelectList(_context.Menu.ToList(), "Id", "Baslik");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,IcecekAdi,Aciklama,Fiyat,MenuId")] MenuCaylarveDigerleri menucaylarvedigerleri)
    {
        if (ModelState.IsValid)
        {
            _context.Add(menucaylarvedigerleri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Menuler = new SelectList(_context.Menu.ToList(), "Id", "Baslik", menucaylarvedigerleri.MenuId);
        return View(menucaylarvedigerleri);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menucaylarvedigerleri = await _context.MenuCaylarveDigerleri.FindAsync(id);
        if (menucaylarvedigerleri == null)
        {
            return NotFound();
        }
        ViewBag.Menuler = new SelectList(_context.Menu.ToList(), "Id", "Baslik", menucaylarvedigerleri.MenuId);
        return View(menucaylarvedigerleri);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,IcecekAdi,Aciklama,Fiyat,MenuId")] MenuCaylarveDigerleri menucaylarvedigerleri)
    {
        if (id != menucaylarvedigerleri.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(menucaylarvedigerleri);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuCaylarveDigerleriExists(menucaylarvedigerleri.Id))
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
        ViewBag.Menuler = new SelectList(_context.Menu.ToList(), "Id", "Baslik", menucaylarvedigerleri.MenuId);
        return View(menucaylarvedigerleri);
    }

    // GET: MENUCAYLARVEDIGERLERIS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menucaylarvedigerleri = await _context.MenuCaylarveDigerleri
            .FirstOrDefaultAsync(m => m.Id == id);
        if (menucaylarvedigerleri == null)
        {
            return NotFound();
        }

        return View(menucaylarvedigerleri);
    }

    // POST: MENUCAYLARVEDIGERLERIS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var menucaylarvedigerleri = await _context.MenuCaylarveDigerleri.FindAsync(id);
        if (menucaylarvedigerleri != null)
        {
            _context.MenuCaylarveDigerleri.Remove(menucaylarvedigerleri);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MenuCaylarveDigerleriExists(int? id)
    {
        return _context.MenuCaylarveDigerleri.Any(e => e.Id == id);
    }
}
