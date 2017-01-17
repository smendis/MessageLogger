using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLogger.Web.Models
{
    public class LogRequestModel
    {
        [Required]
        [StringLength(32)]
        [Display(Name ="Application ID")]
        public string application_id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Logger Name")]
        public string logger { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Log Level")]
        public string level { get; set; }

        [Required]
        [StringLength(2048)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Log Message")]
        public string message { get; set; }
    }
}