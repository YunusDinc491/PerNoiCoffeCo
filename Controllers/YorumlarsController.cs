using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;
using PerNoiWebsite.Context;

[Authorize]
public class YorumlarsController : Controller
{
    private readonly AppDbContext _context;

    public YorumlarsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: YORUMLARS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Yorumlar.ToListAsync());
    }

    // GET: YORUMLARS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var yorumlar = await _context.Yorumlar
            .FirstOrDefaultAsync(m => m.Id == id);
        if (yorumlar == null)
        {
            return NotFound();
        }

        return View(yorumlar);
    }

    // GET: YORUMLARS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: YORUMLARS/Create
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Baslik,Yorum,KisiAdi,Lakap,Photo")] Yorumlar yorumlar)
    {
        if (ModelState.IsValid)
        {
            _context.Add(yorumlar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(yorumlar);
    }

    // GET: YORUMLARS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var yorumlar = await _context.Yorumlar.FindAsync(id);
        if (yorumlar == null)
        {
            return NotFound();
        }
        return View(yorumlar);
    }

    // POST: YORUMLARS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Baslik,Yorum,KisiAdi,Lakap,Photo")] Yorumlar yorumlar)
    {
        if (id != yorumlar.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(yorumlar);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YorumlarExists(yorumlar.Id))
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
        return View(yorumlar);
    }

    // GET: YORUMLARS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var yorumlar = await _context.Yorumlar
            .FirstOrDefaultAsync(m => m.Id == id);
        if (yorumlar == null)
        {
            return NotFound();
        }

        return View(yorumlar);
    }

    // POST: YORUMLARS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var yorumlar = await _context.Yorumlar.FindAsync(id);
        if (yorumlar != null)
        {
            _context.Yorumlar.Remove(yorumlar);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool YorumlarExists(int? id)
    {
        return _context.Yorumlar.Any(e => e.Id == id);
    }
}
