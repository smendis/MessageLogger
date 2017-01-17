using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MessageLogger.Data
{
    [Table("log")]
    public class Log
    {
        [Key]
        public int log_id { get; set; }
        public string logger { get; set; }
        public string level { get; set; }
        public string message { get; set; }
        public string application_id { get; set; }

        public virtual Application application { get; set; }
    }
}
