using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLogger.Api.Models
{
    /// <summary>
    /// The response to a logging request.
    /// </summary>
    public class LogResponseModel
    {
        /// <summary>
        /// Indicates whether the log storage request was successful.
        /// </summary>
        public bool success { get; set; }

        public LogResponseModel(bool state)
        {
            success = state;
        }
    }
}