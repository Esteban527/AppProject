using AppProject.Web.Data.Entities;
using AppProject.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppProject.Web.Controllers
{
    public class TipCalculatorsController : Controller
    {
        private readonly ITipCalculatorService _tipCalculatorService;

        public TipCalculatorsController(ITipCalculatorService tipCalculatorService)
        {
            _tipCalculatorService = tipCalculatorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TipCalculator model)
        {
            if (ModelState.IsValid)
            {
                model.Tip = await _tipCalculatorService.CalculateTipAsync(model.BillTotal, model.TipPercentage);
            }
            return View(model);
        }
    }
}
