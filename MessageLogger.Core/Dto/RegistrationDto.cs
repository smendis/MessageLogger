using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Core.Dto
{
    /// <summary>
    /// Registration Response.
    /// </summary>
   public class RegistrationDto
    {
        public RegistrationDto()
        {

        }

        public RegistrationDto(string displayName)
        {
            this.application_id = Guid.NewGuid().ToString("N");
            this.display_name = displayName;
            this.secret = Guid.NewGuid().ToString("N").Substring(0, 25);
        }

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
        /// The 25 character secret for which to create the token.
        /// </summary>
        [StringLength(25)]
        public string secret { get; set; }
    }
}
