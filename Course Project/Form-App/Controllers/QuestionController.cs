using Form_App.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Form_App.Data;
using Form_App.Entity;
using Microsoft.AspNetCore.Identity;

namespace Form_App.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Question/Create/5
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var template = await _context.Templates.FindAsync(id);
            if (template == null)
            {
                return NotFound();
            }
            // Check if the user is the creator
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && template.CreatorId != userId)
            {
                return Forbid();
            }
            // Create view model
            var viewModel = new CreateQuestionViewModel
            {
                TemplateId = template.Id
            };
            return View(viewModel);
        }


        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuestionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var template = await _context.Templates
                    .Include(t => t.Questions)
                    .FirstOrDefaultAsync(t => t.Id == viewModel.TemplateId);
                if (template == null)
                {
                    return NotFound();
                }
                // Check if the user is the creator
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin");
                if (!isAdmin && template.CreatorId != userId)
                {
                    return Forbid();
                }
                // Create and set up the question entity
                var question = new Question
                {
                    TemplateId = viewModel.TemplateId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    QuestionType = viewModel.QuestionType,
                    ShowInResults = viewModel.ShowInResults
                };
                // Set the order index for the new question
                if (template.Questions.Any())
                {
                    question.OrderIndex = template.Questions.Max(q => q.OrderIndex) + 1;
                }
                else
                {
                    question.OrderIndex = 0;
                }
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Template", new { id = question.TemplateId });
            }
            return View(viewModel);
        }


        // GET: Question/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _context.Questions
                .Include(q => q.Template)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            // Check if the user is the creator
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && question.Template.CreatorId != userId)
            {
                return Forbid();
            }
            // Map to view model
            var viewModel = new EditQuestionViewModel
            {
                Id = question.Id,
                TemplateId = question.TemplateId,
                Title = question.Title,
                Description = question.Description,
                QuestionType = question.QuestionType,
                ShowInResults = question.ShowInResults
            };
            return View(viewModel);
        }


        // POST: Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditQuestionViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }
            var question = await _context.Questions
                .Include(q => q.Template)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            // Check if the user is the creator
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && question.Template.CreatorId != userId)
            {
                return Forbid();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Update question from view model
                    question.Title = viewModel.Title;
                    question.Description = viewModel.Description;
                    question.QuestionType = viewModel.QuestionType;
                    question.ShowInResults = viewModel.ShowInResults;

                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Template", new { id = question.TemplateId });
            }
            return View(viewModel);
        }


        // Post: Question/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _context.Questions
                .Include(q => q.Template)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            // Check if the user is the creator
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && question.Template.CreatorId != userId)
            {
                return Forbid();
            }
            var templateId = question.TemplateId;
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "Template", new { id = templateId });
        }


        // Post : Question/MoveQuestionUp/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveQuestionUp(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _context.Questions
                .Include(q => q.Template)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            // Check if the user is the creator
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && question.Template.CreatorId != userId)
            {
                return Forbid();
            }
            // Get the previous question in the order
            var previousQuestion = await _context.Questions
                .Where(q => q.TemplateId == question.TemplateId && q.OrderIndex < question.OrderIndex)
                .OrderByDescending(q => q.OrderIndex)
                .FirstOrDefaultAsync();
            if (previousQuestion != null)
            {
                var tempQuestion = question.OrderIndex;
                question.OrderIndex = previousQuestion.OrderIndex;
                previousQuestion.OrderIndex = tempQuestion;
                _context.Update(question);
                _context.Update(previousQuestion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit", "Template", new { id = question.TemplateId });
        }


        // Post : Question/MoveQuestionDown/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveQuestionDown(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _context.Questions
                .Include(q => q.Template)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            // Check if the user is the creator
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && question.Template.CreatorId != userId)
            {
                return Forbid();
            }
            // Get the next question in the order
            var nextQuestion = await _context.Questions
                .Where(q => q.TemplateId == question.TemplateId && q.OrderIndex > question.OrderIndex)
                .OrderBy(q => q.OrderIndex)
                .FirstOrDefaultAsync();
            if (nextQuestion != null)
            {
                var tempQuestion = question.OrderIndex;
                question.OrderIndex = nextQuestion.OrderIndex;
                nextQuestion.OrderIndex = tempQuestion;
                _context.Update(question);
                _context.Update(nextQuestion);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit", "Template", new { id = question.TemplateId });
        }


        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
