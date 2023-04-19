using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace job_offers.Models
{
    public class category
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "kind of job")]
        public string categoryname { get; set; }

        [Required]
        [Display(Name = "discription of job ")]
        public string categorydicrption { get; set; }

        public virtual ICollection<job> jobs { get; set; }
    }
}