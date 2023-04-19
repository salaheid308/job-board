using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace job_offers.Models
{
    public class job
    {
        public int id { get; set; }
        
        [Display(Name = "job name")]
        public string jobtitle { get; set; }

        [Display(Name = "image of job ")]
        public string jobimg { get; set; }

        [Display(Name = "job information ")]
        [AllowHtml]
        public string jobdiscrption { get; set; }

        [Display(Name = "job tybe")]
        public int categoryid { get; set; }

        public string userid { get; set; }

        public virtual category category { get; set; }
        public virtual ApplicationUser user { get; set; }

    }
}