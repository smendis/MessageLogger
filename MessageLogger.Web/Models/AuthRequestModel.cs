using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLogger.Web.Models
{
    public class AuthRequestModel
    {
        [Required]
        [Display(Name ="Application ID")]
        [StringLength(32)]
        public string application_id { get; set; }

        [Required]
        [Display(Name = "Application Secret")]
        [StringLength(25)]
        public string secret { get; set; }

        public string GetAuthorizationHeaderValue()
        {
            return string.Concat(application_id, ":", secret);
        }
    }
}