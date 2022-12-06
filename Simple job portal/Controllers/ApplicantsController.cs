using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simple_job_portal.Data.Services;
using Simple_job_portal.Models;

namespace Simple_job_portal.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IApplicantsService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicantsController(IApplicantsService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }
        public async Task<IActionResult> Applicants()
        {
            var user = await _userManager.GetUserAsync(User);
            var Apps = _service.GetAllApplicants(user);
            return View(Apps);
        }

        public IActionResult ApplicantsByJob(int id)
        {
            var model = _service.GetApplicationByJob(id);
            return View(model);
        }
    }
}
