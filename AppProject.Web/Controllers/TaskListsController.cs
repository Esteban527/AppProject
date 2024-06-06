using AppProject.Web.Core;
using AppProject.Web.Data;
using AppProject.Web.Data.Entities;
using AppProject.Web.DTOs;
using AppProject.Web.Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppProject.Web.Controllers
{
    public class TaskListsController : Controller
    {
        private readonly DataContext _context;
        private readonly ITaskListsService _taskListService;
        private readonly INotyfService _notifyService;

        public TaskListsController(DataContext context, ITaskListsService taskListService, INotyfService notifyService)
        {
            _context = context;
            _taskListService = taskListService;
            _notifyService = notifyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Response<List<TaskList>> response = await _taskListService.GetListAsyc();

            if (!response.IsSuccess)
            {
                _notifyService.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            _notifyService.Success(response.Message);
            return View(response.Result);
        }

       [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskListDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                TaskList taskList = new TaskList
                {
                    Description = dto.Description,
                    Iscompleted = dto.Iscompleted
                    
                };

                await _context.TaskLists.AddAsync(taskList);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            try
            {
                TaskList taskList = await _context.TaskLists.FirstOrDefaultAsync(t => t.Id == id);

                if (taskList is null)
                {
                    return RedirectToAction(nameof(Index));
                }

                TaskListDTO dto = new TaskListDTO
                {
                    Id = id,
                    Description = taskList.Description,
                    Iscompleted = taskList.Iscompleted
                };

                return View(dto);

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(TaskListDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                TaskList taskList = await _context.TaskLists.FirstOrDefaultAsync(t => t.Id == dto.Id);

                if (taskList is null)
                {
                    return NotFound();
                }

                taskList.Description = dto.Description;

                _context.TaskLists.Update(taskList);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                TaskList taskList = await _context.TaskLists.FirstOrDefaultAsync(a => a.Id == id);

                if (taskList is null)
                {
                    return RedirectToAction(nameof(Index));
                }

                _context.TaskLists.Remove(taskList);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
