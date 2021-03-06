﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageLogger.Api.Models
{
    /// <summary>
    /// The Authentication response model.
    /// </summary>
    public class AuthResponseModel
    {
        /// <summary>
        /// The 32 character id for which to create the token.
        /// </summary>
        public string access_token { get; set; }

        public AuthResponseModel(string token)
        {
            access_token = token;
        }
    }
}