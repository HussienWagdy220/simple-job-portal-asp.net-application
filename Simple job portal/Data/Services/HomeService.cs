using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data.Services
{
    public class HomeService : IHomeService
    {
        private readonly AppDbContext _context;

        public HomeService(AppDbContext context)
        {
            _context = context;           
        }
        public TrendingJobViewModel AllJobs()
        {
            var jobs = _context.Jobs.Where(x => x.Filled == false).ToList();

            var trendings = _context.Jobs
                .Where(x => x.CreatedAt.Month == DateTime.Now.Month)
                .Where(x => x.Filled == false)
                .ToList();

            var model = new TrendingJobViewModel()
            {
                Trendings = trendings,
                Jobs = jobs
            };

            return model;
        }

        public JobDetailsViewModel JobDetails(int id, ApplicationUser user)
        {
           
            var job = _context.Jobs.FirstOrDefault(x => x.Id == id);
            var applied = false;

            if(user != null)
            {
                applied = _context.Applicants.Where(x => x.Jop == job).Any(x => x.User == user);
            }

            var model = new JobDetailsViewModel()
            {
                Job = job,
                IsApplied = applied
            };

            return model;
        }
    }
}
