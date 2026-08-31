using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerNoiWebsite.Models;
using PerNoiWebsite.Context;
[Authorize]
public class MenusController : Controller
{
    private readonly AppDbContext _context;

    public MenusController(AppDbContext context)
    {
        _context = context;
    }

    // GET: MENUS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Menu.ToListAsync());
    }

    // GET: MENUS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menu = await _context.Menu
            .FirstOrDefaultAsync(m => m.Id == id);
        if (menu == null)
        {
            return NotFound();
        }

        return View(menu);
    }

    // GET: MENUS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MENUS/Create
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Baslik")] Menu menu)
    {
        if (ModelState.IsValid)
        {
            _context.Add(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(menu);
    }

    // GET: MENUS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menu = await _context.Menu.FindAsync(id);
        if (menu == null)
        {
            return NotFound();
        }
        return View(menu);
    }

    // POST: MENUS/Edit/5
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Baslik")] Menu menu)
    {
        if (id != menu.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(menu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(menu.Id))
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
        return View(menu);
    }

    // GET: MENUS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var menu = await _context.Menu
            .FirstOrDefaultAsync(m => m.Id == id);
        if (menu == null)
        {
            return NotFound();
        }

        return View(menu);
    }

    // POST: MENUS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var menu = await _context.Menu.FindAsync(id);
        if (menu != null)
        {
            _context.Menu.Remove(menu);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MenuExists(int? id)
    {
        return _context.Menu.Any(e => e.Id == id);
    }
}
