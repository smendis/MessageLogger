using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLogger.Api.Models
{
    /// <summary>
    /// The response to a register request.
    /// </summary>
    public class RegisterResponseModel
    {

        /// <summary>
        /// The 32 character id for which to create the token.
        /// </summary>
        ///
        [StringLength(32)]
        public string application_id { get; set; }

        /// <summary>
        /// Up to 32 characters representing the applications name.
        /// </summary>
        [StringLength(32)]
        public string display_name { get; set; }

        /// <summary>
        /// The 32 character secret for which to create the token.
        /// </summary>
        [StringLength(32)]
        public string application_secret { get; set; }
    }
}