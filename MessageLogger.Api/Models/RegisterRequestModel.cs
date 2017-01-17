using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLogger.Api.Models
{
    /// <summary>
    /// Register a new application within the Logger Service, unauthenticated end point.
    /// </summary>
    public class RegisterRequestModel
    {
        /// <summary>
        /// Up to 32 characters representing the applications’ name.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string display_name { get; set; }
    }
}