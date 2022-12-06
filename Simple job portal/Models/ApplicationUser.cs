using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Simple_job_portal.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        [MaxLength(50)]
        public string FullName { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
    }
}
