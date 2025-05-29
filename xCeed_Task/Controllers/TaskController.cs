using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Layer.Services.Tasks;
using Service.Layer.ViewModels.Tasks;

namespace xCeed_Task.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<AppUser> _userManager;

        public TaskController(ITaskService taskService, UserManager<AppUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        // GET: TaskController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var task = await _taskService.GetTask(id);

            return View(task);
        }

        // GET: TaskController/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(Guid Id)
        {
            var task = await _taskService.GetTask(Id);

            if (task == null)
            {
                return NotFound();
            }

            var users = await _userManager.Users.ToListAsync();

            ViewBag.Users = new SelectList(users, "Id", "DisplayName");

            return View(task);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(TaskVM taskVM)
        {
            try
            {
                var result = await _taskService.UpdateTask(taskVM);
                return Json(result);
            }
            catch
            {
                return Json(new { Status = false });
            }
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
