using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simple_job_portal.Data.Services;
using Simple_job_portal.Models;
using System.Diagnostics;

namespace Simple_job_portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IHomeService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = _service.AllJobs();
            return View(model);
        }

        public async Task<IActionResult> JobDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var model = _service.JobDetails(id, user);
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}