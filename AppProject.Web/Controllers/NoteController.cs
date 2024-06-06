using AppProject.Web.Data.Entities;
using AppProject.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppProject.Web.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly ICategoryService _categoryService;

        public NoteController(INoteService noteService, ICategoryService categoryService)
        {
            _noteService = noteService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var notes = categoryId.HasValue ? await _noteService.GetNotesByCategoryAsync(categoryId.Value) : await _noteService.GetAllNotesAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewData["Categories"] = categories;
            return View(notes);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                await _noteService.AddNoteAsync(note);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Name", note.CategoryId);
            return View(note);
        }
    }
}
