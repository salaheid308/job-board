using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace job_offers.Models
{
    public class jobsview
    {
        public string  jobtitle { get; set; }
        public IEnumerable<applyforjob> items { get; set; }
    }
}