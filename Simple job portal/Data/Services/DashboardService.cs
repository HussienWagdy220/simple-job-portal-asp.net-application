using Microsoft.EntityFrameworkCore;
using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        public DashboardService(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Applicant> GetAllApplicants(ApplicationUser user)
        {
            var applicants = _context.Applicants.Where(x => x.Jop.User == user).Include(x => x.User).Include(x => x.Jop).ToList();
            return applicants;
        }

        public IEnumerable<Job> GetAllJobs(ApplicationUser user)
        {
            var jobs = _context.Jobs.Where(x => x.User == user).Include(x => x.Applicants).ToList();
            return jobs;
        }

        public JobApplicantsViewModel GetAppByJobId(int id)
        {
            var job = _context.Jobs.Include(x => x.Applicants)
                                        .ThenInclude(a => a.User)
                                   .FirstOrDefault(x => x.Id == id);

            var model = new JobApplicantsViewModel()
            {
                Job = job,
                Applicants = job.Applicants
            };

            return model;
        }
    }
}
