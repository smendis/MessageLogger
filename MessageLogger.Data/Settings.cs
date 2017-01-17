using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageLogger.Data
{
    [Table("settings")]
    public class Settings
    {
        [Key]
        public int setting_id { get; set; }
        public string setting_name { get; set; }
        public int setting_value { get; set; }
    }
}
