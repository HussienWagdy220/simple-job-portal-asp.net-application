namespace Simple_job_portal.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public ApplicationUser User{ get; set; }
        public Job Jop { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
