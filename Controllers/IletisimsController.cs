using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;
using PerNoiWebsite.Context;

[Authorize]
public class IletisimsController : Controller
{
    private readonly AppDbContext _context;

    public IletisimsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: ILETISIMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Iletisim.ToListAsync());
    }

    // GET: ILETISIMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var iletisim = await _context.Iletisim
            .FirstOrDefaultAsync(m => m.Id == id);
        if (iletisim == null)
        {
            return NotFound();
        }

        return View(iletisim);
    }

    // GET: ILETISIMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ILETISIMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Baslik,AltMetin,Adres,Telefon,CalismaSaatleri")] Iletisim iletisim)
    {
        if (ModelState.IsValid)
        {
            _context.Add(iletisim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(iletisim);
    }

    // GET: ILETISIMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var iletisim = await _context.Iletisim.FindAsync(id);
        if (iletisim == null)
        {
            return NotFound();
        }
        return View(iletisim);
    }

    // POST: ILETISIMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Baslik,AltMetin,Adres,Telefon,CalismaSaatleri")] Iletisim iletisim)
    {
        if (id != iletisim.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(iletisim);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IletisimExists(iletisim.Id))
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
        return View(iletisim);
    }

    // GET: ILETISIMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var iletisim = await _context.Iletisim
            .FirstOrDefaultAsync(m => m.Id == id);
        if (iletisim == null)
        {
            return NotFound();
        }

        return View(iletisim);
    }

    // POST: ILETISIMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var iletisim = await _context.Iletisim.FindAsync(id);
        if (iletisim != null)
        {
            _context.Iletisim.Remove(iletisim);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool IletisimExists(int? id)
    {
        return _context.Iletisim.Any(e => e.Id == id);
    }
}
