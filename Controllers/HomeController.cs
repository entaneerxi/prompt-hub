using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromptHub.Data;
using PromptHub.Models;

namespace PromptHub.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var prompts = await _context.Prompts
            .Include(p => p.Category)
            .Include(p => p.PromptTags)
            .ThenInclude(pt => pt.Tag)
            .OrderByDescending(p => p.CreatedAt)
            .Take(20)
            .ToListAsync();

        return View(prompts);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
