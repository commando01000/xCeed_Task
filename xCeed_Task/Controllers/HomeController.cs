using System.Diagnostics;
using Data.Layer.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Layer.Specifications.Tasks;
using Service.Layer.Services.Account;
using Service.Layer.Services.Tasks;
using Service.Layer.ViewModels.Home;
using Service.Layer.ViewModels.Tasks;
using xCeed_Task.CustomFilters;
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

        [NoCache]
        public async Task<IActionResult> Index()
        {
            var userId = _accountService.GetCurrentUserId();
            var role = await _accountService.GetCurrentUserRole();

            var HomeVM = new HomeVM();

            if (role == "Admin")
            {
                var specs = new TasksSpecifications();
                var tasks = await _taskService.GetAllTasksPaginated(specs); // for server side pagination
                HomeVM.PaginatedTasks = tasks;
            }
            else
            {
                var specs = new TasksSpecifications();
                specs.AssignedUserId = userId;
                var tasks = await _taskService.GetAllTasks(specs); // for client side pagination
                HomeVM.TasksList = tasks;
            }
            return View(HomeVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetPaginatedTasks(int page = 1, int pageSize = 5, string priority = null)
        {
            var specs = new TasksSpecifications();
            specs.PageIndex = page;
            specs.PageSize = pageSize;

            if (!string.IsNullOrEmpty(priority))
            {
                specs.Priority = (Data.Layer.Entities.TaskPriority)Enum.Parse(typeof(TaskPriority), priority);
            }

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
