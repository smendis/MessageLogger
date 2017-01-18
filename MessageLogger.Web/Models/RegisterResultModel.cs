using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLogger.Web.Models
{
    public class RegisterResultModel
    {
        public string application_id { get; set; }
        public string display_name { get; set; }
        public string application_secret { get; set; }
    }
}