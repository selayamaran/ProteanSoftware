using System;
using System.Security.Policy;

namespace DeveloperTest.Models
{
    public class JobModel
    {
        public int JobId { get; set; }

        public string Engineer { get; set; }

        public DateTime When { get; set; }
        public string Customer { get; set; }
        public string Type { get; set; }
    }
}
