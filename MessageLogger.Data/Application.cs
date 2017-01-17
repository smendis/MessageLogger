using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageLogger.Data
{
    [Table("application")]
    public class Application
    {
        public Application()
        {

        }

        public Application(string applicationId, string displayName, string encryptedSecret)
        {
            this.application_id = applicationId;
            this.display_name = displayName;
            this.secret = encryptedSecret;
        }

        [Key]
        public string application_id { get; set; }
        public string display_name { get; set; }
        public string secret { get; set; }

        public virtual ICollection<Log> logs { get; set; }
    }
}

