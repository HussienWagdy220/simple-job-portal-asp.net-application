using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data.Services
{
    public interface IDashboardService
    {
        IEnumerable<Job> GetAllJobs(ApplicationUser user);

        IEnumerable<Applicant> GetAllApplicants(ApplicationUser user);

        JobApplicantsViewModel GetAppByJobId(int id);


    }
}