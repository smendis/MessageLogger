using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLogger.Web.Models
{
    public class RegisterRequestModel
    {
        [Required]
        [StringLength(32)]
        [Display(Name ="Application Display Name")]
        public string display_name { get; set; }
    }
}