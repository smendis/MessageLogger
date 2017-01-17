using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MessageLogger.Api.Models
{
    /// <summary>
    /// The log request being submitted, through the API.
    /// </summary>
    public class LogRequestModel
    {
        /// <summary>
        /// The id of the connecting application
        /// </summary>
        [StringLength(32)]
        public string application_id { get; set; }

        /// <summary>
        /// The logger.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string logger { get; set; }

        /// <summary>
        /// The Log level.
        /// </summary>
        [Required]
        [StringLength(256)]
        public string level { get; set; }

        /// <summary>
        /// The Actual log message.
        /// </summary>
        [Required]
        [StringLength(2048)]
        public string message { get; set; }
    }
}