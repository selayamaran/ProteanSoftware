using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs
                .Include(j => j.Customer)
                .Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = x.Customer == null ? "" : x.Customer.Name,
                Type = x.Customer == null ? "" : x.Customer.Type
                }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            var result = context.Jobs
                    .Include(j => j.Customer)
                    .Where(x => x.JobId == jobId).Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                When = x.When,
                Customer = x.Customer == null ? "" : x.Customer.Name,
                Type = x.Customer == null ? "" : x.Customer.Type
            }).SingleOrDefault();

            return result;
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var customer = context.Customers.FirstOrDefault(x => x.CustomerId == model.CustomerId);
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                When = model.When,
                CustomerId = model.CustomerId
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When,
                Customer = customer.Name
            };
        }
    }
}
