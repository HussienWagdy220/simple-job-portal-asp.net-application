using Simple_job_portal.Models;
using System.Security.Claims;

namespace Simple_job_portal.Data.Services
{
    public interface IJobsService
    {
        IEnumerable<Job> GetAllJobs();

        void Save(Job job);

        void Apply(int id, ApplicationUser User);

        void MareAsFilled(int id);

        Job GetJobById(int id);

        void DeleteJob(int id);




    }
}
