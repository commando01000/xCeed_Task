using System.Diagnostics;
using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Layer.Specifications.Tasks;
using Service.Layer.Services.Account;
using Service.Layer.Services.Tasks;
using Service.Layer.ViewModels.Home;
using xCeed_Task.Models;

namespace xCeed_Task.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;
        private readonly IAccountService _accountService;

        public HomeController(ILogger<HomeController> logger, ITaskService taskService, UserManager<AppUser> userManager, IAccountService accountService)
        {
            _logger = logger;
            _taskService = taskService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            var userId = _accountService.GetCurrentUserId();
            var role = await _accountService.GetCurrentUserRole();

            var HomeVM = new HomeVM();

            if (role == "Admin")
            {
                var specs = new TasksSpecifications();
                var tasks = await _taskService.GetAllTasksPaginated(specs);
                HomeVM.PaginatedTasks = tasks;
            }
            else
            {
                var specs = new TasksSpecifications();
                specs.AssignedUserId = userId;
                var tasks = await _taskService.GetAllTasks(specs);
                HomeVM.TasksList = tasks;
            }
            return View(HomeVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetPaginatedTasks(int page = 1, int pageSize = 5)
        {
            var specs = new TasksSpecifications();
            specs.PageIndex = page;
            specs.PageSize = pageSize;
            var tasks = await _taskService.GetAllTasksPaginated(specs);
            return PartialView("~/Views/PartialViews/_AdminTasks.cshtml", tasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error(string? message = null)
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = message
            };
            return View(model);
        }
    }
}
