using Simple_job_portal.Models;

namespace Simple_job_portal.Data.ViewModels
{
    public class JobApplicantsViewModel
    {
        public Job Job { get; set; }

        public List<Applicant> Applicants { get; set; }
    }
}
