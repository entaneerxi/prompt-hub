using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromptHub.Data;
using PromptHub.Models;

namespace PromptHub.Controllers;

public class PromptsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PromptsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Prompts
    public async Task<IActionResult> Index(string searchString, int? categoryId, int? tagId)
    {
        ViewData["CurrentFilter"] = searchString;
        ViewData["CategoryId"] = categoryId;
        ViewData["TagId"] = tagId;

        var prompts = _context.Prompts
            .Include(p => p.Category)
            .Include(p => p.PromptTags)
            .ThenInclude(pt => pt.Tag)
            .AsQueryable();

        // Search filter
        if (!string.IsNullOrEmpty(searchString))
        {
            prompts = prompts.Where(p => p.Title.Contains(searchString) || p.Content.Contains(searchString));
        }

        // Category filter
        if (categoryId.HasValue && categoryId.Value > 0)
        {
            prompts = prompts.Where(p => p.CategoryId == categoryId.Value);
        }

        // Tag filter
        if (tagId.HasValue && tagId.Value > 0)
        {
            prompts = prompts.Where(p => p.PromptTags.Any(pt => pt.TagId == tagId.Value));
        }

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Tags = new SelectList(await _context.Tags.ToListAsync(), "Id", "Name");

        return View(await prompts.OrderByDescending(p => p.CreatedAt).ToListAsync());
    }

    // GET: Prompts/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prompt = await _context.Prompts
            .Include(p => p.Category)
            .Include(p => p.PromptTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (prompt == null)
        {
            return NotFound();
        }

        return View(prompt);
    }

    // GET: Prompts/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.Tags = await _context.Tags.ToListAsync();
        return View();
    }

    // POST: Prompts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Content,Description,SourceUrl,CategoryId")] Prompt prompt, int[] selectedTags)
    {
        if (ModelState.IsValid)
        {
            prompt.CreatedAt = DateTime.Now;
            prompt.UpdatedAt = DateTime.Now;
            _context.Add(prompt);
            await _context.SaveChangesAsync();

            // Add tags
            if (selectedTags != null && selectedTags.Length > 0)
            {
                foreach (var tagId in selectedTags)
                {
                    _context.PromptTags.Add(new PromptTag { PromptId = prompt.Id, TagId = tagId });
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", prompt.CategoryId);
        ViewBag.Tags = await _context.Tags.ToListAsync();
        return View(prompt);
    }

    // GET: Prompts/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prompt = await _context.Prompts
            .Include(p => p.PromptTags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (prompt == null)
        {
            return NotFound();
        }

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", prompt.CategoryId);
        ViewBag.Tags = await _context.Tags.ToListAsync();
        ViewBag.SelectedTags = prompt.PromptTags.Select(pt => pt.TagId).ToArray();
        return View(prompt);
    }

    // POST: Prompts/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Description,SourceUrl,CategoryId,CreatedAt")] Prompt prompt, int[] selectedTags)
    {
        if (id != prompt.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                prompt.UpdatedAt = DateTime.Now;
                _context.Update(prompt);

                // Update tags
                var existingTags = _context.PromptTags.Where(pt => pt.PromptId == id);
                _context.PromptTags.RemoveRange(existingTags);

                if (selectedTags != null && selectedTags.Length > 0)
                {
                    foreach (var tagId in selectedTags)
                    {
                        _context.PromptTags.Add(new PromptTag { PromptId = prompt.Id, TagId = tagId });
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromptExists(prompt.Id))
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

        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", prompt.CategoryId);
        ViewBag.Tags = await _context.Tags.ToListAsync();
        return View(prompt);
    }

    // GET: Prompts/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prompt = await _context.Prompts
            .Include(p => p.Category)
            .Include(p => p.PromptTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (prompt == null)
        {
            return NotFound();
        }

        return View(prompt);
    }

    // POST: Prompts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var prompt = await _context.Prompts.FindAsync(id);
        if (prompt != null)
        {
            _context.Prompts.Remove(prompt);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool PromptExists(int id)
    {
        return _context.Prompts.Any(e => e.Id == id);
    }
}
