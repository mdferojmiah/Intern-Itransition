using System.Security.Claims;
using Form_App.Data;
using Form_App.Entity;
using Form_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Form_App.Controllers
{
    [Authorize]
    public class TemplateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TemplateController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Template
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            IQueryable<Template> templates;

            if (isAdmin)
            {
                templates = _context.Templates.Include(t => t.Creator);
            }
            else
            {
                templates = _context.Templates
                    .Include(t => t.Creator)
                    .Where(t => t.CreatorId == userId || t.IsPublic);
            }

            // Map to view model
            var viewModels = await templates.Select(t => new TemplateViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsPublic = t.IsPublic,
                CreatorName = t.Creator.UserName,
                CreationDate = t.CreationDate
            }).ToListAsync();

            return View(viewModels);
        }

        // GET: Template/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.Creator)
                .Include(t => t.Questions.OrderBy(q => q.OrderIndex))
                .FirstOrDefaultAsync(m => m.Id == id);

            if (template == null)
            {
                return NotFound();
            }

            // Check if the user is the creator or if the template is public
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && template.CreatorId != userId && !template.IsPublic)
            {
                return Forbid();
            }

            // Map to view model
            var viewModel = new TemplateViewModel
            {
                Id = template.Id,
                Title = template.Title,
                Description = template.Description,
                IsPublic = template.IsPublic,
                CreatorName = template.Creator.UserName,
                CreationDate = template.CreationDate,
                // Add Questions to the view model
                Questions = template.Questions.Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    TemplateId = q.TemplateId,
                    Title = q.Title,
                    Description = q.Description,
                    QuestionType = q.QuestionType,
                    ShowInResults = q.ShowInResults,
                    OrderIndex = q.OrderIndex
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: Template/Create
        public IActionResult Create()
        {
            return View(new CreateTemplateViewModel());
        }

        // POST: Template/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTemplateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                var template = new Template
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    IsPublic = viewModel.IsPublic,
                    CreatorId = userId,
                    Creator = user,
                    CreationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                _context.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { id = template.Id });
            }
            return View(viewModel);
        }

        // GET: Template/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .Include(t => t.Questions.OrderBy(q => q.OrderIndex))
                .FirstOrDefaultAsync(t => t.Id == id);

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

            // Map to view model
            var viewModel = new EditTemplateViewModel
            {
                Id = template.Id,
                Title = template.Title,
                Description = template.Description,
                IsPublic = template.IsPublic,
                Questions = template.Questions.Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    TemplateId = q.TemplateId,
                    Title = q.Title,
                    Description = q.Description,
                    QuestionType = q.QuestionType,
                    ShowInResults = q.ShowInResults,
                    OrderIndex = q.OrderIndex
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Template/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditTemplateViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            var template = await _context.Templates.FindAsync(id);

            if (template == null)
            {
                return NotFound();
            }

            // Check permissions
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && template.CreatorId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update template properties from view model
                    template.Title = viewModel.Title;
                    template.Description = viewModel.Description;
                    template.IsPublic = viewModel.IsPublic;

                    _context.Update(template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.Id))
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

            // If we get here, something went wrong - reload questions for the view
            var questions = await _context.Questions
                .Where(q => q.TemplateId == id)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();

            viewModel.Questions = questions.Select(q => new QuestionViewModel
            {
                Id = q.Id,
                TemplateId = q.TemplateId,
                Title = q.Title,
                Description = q.Description,
                QuestionType = q.QuestionType,
                ShowInResults = q.ShowInResults,
                OrderIndex = q.OrderIndex
            }).ToList();

            return View(viewModel);
        }

        // GET: Template/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var template = await _context.Templates
                .Include(t => t.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);

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

            // Map to view model
            var viewModel = new TemplateViewModel
            {
                Id = template.Id,
                Title = template.Title,
                Description = template.Description,
                IsPublic = template.IsPublic,
                CreatorName = template.Creator.UserName,
                CreationDate = template.CreationDate
            };

            return View(viewModel);
        }

        // POST: Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Template/AddQuestion/5
        public async Task<IActionResult> AddQuestion(int? id)
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

        // POST: Template/AddQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion(CreateQuestionViewModel viewModel)
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
                return RedirectToAction(nameof(Edit), new { id = question.TemplateId });
            }
            return View(viewModel);
        }

        // GET: Template/EditQuestion/5
        public async Task<IActionResult> EditQuestion(int? id)
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

        // POST: Template/EditQuestion/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuestion(int id, EditQuestionViewModel viewModel)
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
                return RedirectToAction(nameof(Edit), new { id = question.TemplateId });
            }
            return View(viewModel);
        }

        // Post: Template/DeleteQuestion/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteQuestion(int id)
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
            return RedirectToAction(nameof(Edit), new { id = templateId });
        }

        // Post : Template/MoveQuestionUp/5
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

            return RedirectToAction(nameof(Edit), new { id = question.TemplateId });
        }

        // Post : Template/MoveQuestionDown/5
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

            return RedirectToAction(nameof(Edit), new { id = question.TemplateId });
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }
        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}