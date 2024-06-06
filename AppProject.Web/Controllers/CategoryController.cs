using AppProject.Web.Data.Entities;
using AppProject.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppProject.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INoteService _noteService;

        public CategoryController(ICategoryService categoryService, INoteService noteService)
        {
            _categoryService = categoryService;
            _noteService = noteService;
        }
        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var notes = categoryId.HasValue ? await _noteService.GetNotesByCategoryAsync(categoryId.Value) : await _noteService.GetAllNotesAsync();
            ViewData["Categories"] = categories;
            return View(notes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
