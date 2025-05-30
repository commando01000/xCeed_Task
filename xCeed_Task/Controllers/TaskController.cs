using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Layer.Services.Tasks;
using Service.Layer.ViewModels.Tasks;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            
            // show all users for admin otherwise show only current user (to be assignable)
            if (User.IsInRole("Admin"))
            {
                var users = await _userManager.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Id", "DisplayName");
            }
            else
            {
                ViewBag.Users = new SelectList(new[]
                {
                        new { Id = currentUser?.Id, DisplayName = currentUser?.DisplayName }
                }, "Id", "DisplayName");
            }

            return View();
        }

        // POST: TaskController/Create
        [HttpPost]
        public async Task<JsonResult> Create(TaskVM taskVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _taskService.AddTask(taskVM);
                    return Json(result);
                }
                else
                {
                    var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                    return Json(new { Status = false, Errors = errors });
                }

            }
            catch
            {
                return Json(new { Status = false });
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
        public async Task<JsonResult> Delete(Guid id)
        {
            try
            {
                var result = await _taskService.DeleteTask(id);

                return Json(result);
            }
            catch
            {
                return Json(new { Status = false, Errors = new string[] { "Something went wrong" } });
            }
        }
    }
}
