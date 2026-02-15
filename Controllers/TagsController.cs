using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromptHub.Data;
using PromptHub.Models;

namespace PromptHub.Controllers;

public class TagsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Tags
    public async Task<IActionResult> Index()
    {
        return View(await _context.Tags.OrderBy(t => t.Name).ToListAsync());
    }

    // GET: Tags/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tags/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name")] Tag tag)
    {
        if (ModelState.IsValid)
        {
            tag.CreatedAt = DateTime.Now;
            _context.Add(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tag);
    }

    // GET: Tags/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            return NotFound();
        }
        return View(tag);
    }

    // POST: Tags/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedAt")] Tag tag)
    {
        if (id != tag.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(tag.Id))
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
        return View(tag);
    }

    // GET: Tags/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _context.Tags
            .FirstOrDefaultAsync(m => m.Id == id);
        if (tag == null)
        {
            return NotFound();
        }

        return View(tag);
    }

    // POST: Tags/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag != null)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool TagExists(int id)
    {
        return _context.Tags.Any(e => e.Id == id);
    }
}
