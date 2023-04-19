using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace job_offers.Models
{
    public class applyforjob
    {
        public int id { get; set; }
        public string  message { get; set; }
        public DateTime applydate { get; set; }
        public int jobid { get; set; }
        public string  userid { get; set; }

        public virtual job job { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}