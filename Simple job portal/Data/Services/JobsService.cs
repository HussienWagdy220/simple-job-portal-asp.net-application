using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simple_job_portal.Models;
using System.Security.Claims;

namespace Simple_job_portal.Data.Services
{
    public class JobsService : IJobsService
    {
        private readonly AppDbContext _context;
        
        public JobsService(AppDbContext context)
        {
                _context = context;
        }

        

        public IEnumerable<Job> GetAllJobs()
        {
            var Alljobs = _context.Jobs.ToList();
            return Alljobs;
        }

        public void Save(Job job)
        {
           _context.Jobs.Add(job);
           _context.SaveChanges();
        }

        public void Apply(int id, ApplicationUser User)
        {
            var job = _context.Jobs.FirstOrDefault(j=>j.Id == id);

            var applicant = new Applicant
            {
                User = User,
                Jop = job,
                CreatedAt = DateTime.Now
            };

            _context.Applicants.Add(applicant);
            _context.SaveChanges();
        }

        public void MareAsFilled(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j=>j.Id == id);

            job.Filled = true;
            _context.Jobs.Update(job);
            _context.SaveChanges();
        }

        public Job GetJobById(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
            return job;
        }

        public void DeleteJob(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
            _context.Jobs.Remove(job);
            _context.SaveChanges();
        }
    }
}
