using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Simple_job_portal.Models
{
    public class Job
    {
        public int Id { get; set; }
        [MaxLength(255), Display(Name = "Job Title", Prompt = "Job Title")]
        public string Title { get; set; }
        [Display(Name = "Job Description", Prompt = "Job Description")]
        public string Description { get; set; }
        [Display(Name = "Location", Prompt = "Location")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Type is required"), Display(Name = "Type", Prompt = "Type")]
        public string Type { get; set; }
        [Required, Display(Name = "Last Date", Prompt = "Last Date")]
        public DateTime LastDate { get; set; }
        [Display(Name = "Company Name", Prompt = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Company Description", Prompt = "Company Description")]
        public string CompanyDescription { get; set; }
        [Display(Name = "Website", Prompt = "Website")]
        [Url]
        public string Website { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Filled { get; set; } = false;

        //Relationship
        public ApplicationUser User { get; set; }
        public List<Applicant> Applicants { get; set; }
    }
}
