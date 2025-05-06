using System.Diagnostics;
using Form_App.Data;
using Form_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Form_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get latest public templates
            var latestTemplates = await _context.Templates
                 .Where(t => t.IsPublic)
                 .Include(t => t.Creator)
                .OrderByDescending(t => t.CreationDate)
                .Take(5)
                .ToListAsync();
            // Get most popular templates
            var popularTemplates = await _context.Templates
                .Where(t => t.IsPublic)
                .Include(t => t.Creator)
                .Include(t => t.Forms)
                .OrderByDescending(t => t.Forms.Count)
                .Take(5)
                .ToListAsync();
            var viewModel = new HomeViewModel
            {
                LatestTemplates = latestTemplates,
                PopularTemplates = popularTemplates
            };
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }
            // Normalized query
            query = query.Trim().ToLower();
            // Search in template title and description
            var templates = await _context.Templates
                .Where(t => t.IsPublic && (t.Title.ToLower().Contains(query) ||
                            t.Description != null && t.Description.ToLower().Contains(query)))
                .Include(t => t.Creator)
                .ToListAsync();
            // Search in question text
            var questions = await _context.Questions
                .Where(q => q.Title.ToLower().Contains(query) ||
                            q.Description.ToLower().Contains(query))
                .Select(q => q.TemplateId)
                .Distinct()
                .ToListAsync();
            // Get templates that have questions matching the query
            var templatesWithQuestions = await _context.Templates
                .Where(t => t.IsPublic && questions.Contains(t.Id))
                .Include(t => t.Creator)
                .ToListAsync();
            var result = templates
                        .Union(templatesWithQuestions)
                        .ToList();
            var viewModel = new SearchViewModel
            {
                Query = query,
                Results = result
            };
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
