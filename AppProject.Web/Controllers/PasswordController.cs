using AppProject.Web.Data.Entities;
using AppProject.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppProject.Web.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IPasswordService passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        public IActionResult Index()
        {
            return View(new Password());
        }

        [HttpPost]
        public async Task<IActionResult> Generate(Password model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model); // Devuelve la vista "Index" en caso de error
            }

            model.GeneratedPassword = await passwordService.GeneratePasswordAsync(model.Length, model.IncludeLowercase, model.IncludeUppercase, model.IncludeNumbers, model.IncludeSymbols);
            return View("Index", model); // Devuelve la vista "Index" con el modelo actualizado
        }
    }
}