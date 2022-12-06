using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simple_job_portal.Data.Services;
using Simple_job_portal.Models;
using System.Data;

namespace Simple_job_portal.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobsService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public JobController(IJobsService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var AllJobs = _service.GetAllJobs();
            return View(AllJobs);
        }

        [Authorize(Roles = "Employer")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<IActionResult> Save(Job model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.User = user;
            _service.Save(model);

            return RedirectToActionPermanent("Index","Job");
        }
        [HttpPost]
        public async Task<IActionResult> Apply(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return RedirectToActionPermanent("Login", "Account");
            }

            else
            {
                if (!User.IsInRole("Employee"))
                {
                    TempData["message"] = "You can't do this action";
                    return RedirectToActionPermanent("JobDetails", "Home", new { id });
                }
            }
            _service.Apply(id, user);
            TempData["message"] = "successfull process";
            return RedirectToActionPermanent("JobDetails", "Home", new { id });
        }

        public IActionResult MarkAsFilled(int id)
        {
            _service.MareAsFilled(id);
            return RedirectToActionPermanent("Index", "Job");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var job = _service.GetJobById(id);
            if (job == null) return RedirectToActionPermanent("NotFound");

            _service.DeleteJob(id);
            TempData["type"] = "success";
            TempData["message"] = "Job deleted successfully";

            return RedirectToActionPermanent("Index", "Job");
        }

        public IActionResult JobDetails(int id)
        {
            var job = _service.GetJobById(id);
            return View(job);
        }
    }
}
