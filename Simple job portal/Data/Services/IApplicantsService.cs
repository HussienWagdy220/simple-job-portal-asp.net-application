using Simple_job_portal.Data.ViewModels;
using Simple_job_portal.Models;

namespace Simple_job_portal.Data.Services
{
    public interface IApplicantsService
    {
        IEnumerable<Applicant> GetAllApplicants(ApplicationUser User);

        JobApplicantsViewModel GetApplicationByJob(int id);
    }
}
