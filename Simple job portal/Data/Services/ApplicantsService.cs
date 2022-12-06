using Microsoft.EntityFrameworkCore;
using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data.Services
{
    public class ApplicantsService : IApplicantsService
    {
        private readonly AppDbContext _context;

        public ApplicantsService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Applicant> GetAllApplicants(ApplicationUser User)
        {
            var applicants = _context.Applicants.Where(u => u.Jop.User == User).Include(u => u.User).Include(u => u.Jop).ToList();
            return applicants;
        }

        public JobApplicantsViewModel GetApplicationByJob(int id)
        {
            var job = _context.Jobs
                .Include(x => x.Applicants)
                    .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id);

            var model = new JobApplicantsViewModel
            {
                Job = job,
                Applicants = job.Applicants
            };
            return model;

        }
    }
}
