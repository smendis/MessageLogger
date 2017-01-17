using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLogger.Web.Models
{
    public class AuthViewModel
    {
        public AuthResultModel Result { get; set; }
        public string Error { get; set; }
    }
}