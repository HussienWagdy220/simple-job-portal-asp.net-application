using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simple_job_portal.Data.Services;
using Simple_job_portal.Models;
using System.Data;

namespace Simple_job_portal.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(IDashboardService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var jobs = _service.GetAllJobs(user);
            return View(jobs);
        }

        public async Task<IActionResult> Applicants()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var applicants = _service.GetAllApplicants(user);

            return View(applicants);
        }

        public async Task<IActionResult> ApplicantsByJob(int id)
        {
            var model = _service.GetAppByJobId(id);

            return View(model);
        }
    }
}
