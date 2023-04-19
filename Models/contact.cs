using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace job_offers.Models
{
    public class contact
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        [EmailAddress]
        public string emial { get; set; }
        [Required]
        public string message { get; set; }

    }
}