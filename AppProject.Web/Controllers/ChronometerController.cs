using AppProject.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppProject.Web.Controllers
{
    public class ChronometerController : Controller
    {
        private static Chronometer _model = new Chronometer();
        public IActionResult Index()
        {
            if (_model.IsRunning)
            {
                _model.ElapsedTime = DateTime.Now - _model.StartTime;
            }

            return View(_model);
        }

        [HttpPost]
        public IActionResult Start()
        {
            if (!_model.IsRunning)
            {
                _model.StartTime = DateTime.Now - _model.ElapsedTime;
                _model.IsRunning = true;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Pause()
        {
            if (_model.IsRunning)
            {
                _model.ElapsedTime = DateTime.Now - _model.StartTime;
                _model.IsRunning = false;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reset()
        {
            _model = new Chronometer();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Lap()
        {
            if (_model.IsRunning)
            {
                var lapTime = DateTime.Now - _model.StartTime;
                _model.LapTimes.Add(lapTime);
            }
            return RedirectToAction("Index");
        }
    }
}
