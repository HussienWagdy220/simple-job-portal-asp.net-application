using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data.Services
{
    public interface IHomeService
    {
        TrendingJobViewModel AllJobs();

        JobDetailsViewModel JobDetails(int id, ApplicationUser user);
    }
}
